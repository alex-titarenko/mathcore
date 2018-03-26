using System;


namespace TAlex.MathCore.Performance
{
    public class PerformanceManager
    {
        #region Properties

        public static PerformanceManager Current { get; private set; }


        public long MaxArraySize { get; set; }

        public long MaxIterationCount { get; set; }

        #endregion

        #region Constructors

        static PerformanceManager()
        {
            Current = new PerformanceManager();
        }

        protected PerformanceManager()
        {
            MaxArraySize = -1;
            MaxIterationCount = -1;
        }

        #endregion

        #region Methods

        public virtual void EnsureAcceptableArraySize(long size)
        {
            if (MaxArraySize > 0 && size >= MaxArraySize)
            {
                throw new InvalidOperationException(Properties.Resources.EXC_ARRAY_SIZE_IS_TOO_LARGE);
            }
        }

        public virtual void EnsureAcceptableIterationCount(long count)
        {
            if (MaxIterationCount > 0 && count >= MaxIterationCount)
            {
                throw new InvalidOperationException(Properties.Resources.EXC_ITERATION_COUNT_IS_TOO_MUCH);
            }
        }

        #endregion
    }
}
