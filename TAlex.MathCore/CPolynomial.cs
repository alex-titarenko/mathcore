using System;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TAlex.MathCore
{
    /// <summary>
    /// Represents a complex polynomial.
    /// </summary>
    [Serializable]
    public class CPolynomial : ICloneable, IFormattable, IXmlSerializable
    {
        #region Fields

        /// <summary>
        /// Providing data for the complex polynomial.
        /// </summary>
        private Complex[] _coeffs;

        private static readonly string _polyPartPattern = @"(?<sign>[+-]{0})[ \t]*(((?<num>[0-9.,]+i?)|\((?<num>{1})\))\*?)?(?<var>[_a-zA-Z][a-zA-Z0-9]*)?(\^(?<exp>[0-9]+))?";
        private static readonly string _polyFirstPartPattern = String.Format(_polyPartPattern, "?", Complex.ComplexPattern);
        private static readonly string _polyOtherPartsPattern = String.Format(_polyPartPattern, String.Empty, Complex.ComplexPattern);
        private static readonly string _polyPattern = String.Format(@"^(?<term>[ \t]*{0})(?<term>[ \t]*{1})*$", _polyFirstPartPattern, _polyOtherPartsPattern);
        private static readonly Regex _polyRegex = new Regex(_polyPattern, RegexOptions.Compiled);
        private static readonly Regex _polyPartRegex = new Regex(_polyFirstPartPattern, RegexOptions.Compiled);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the coefficient of complex polynomial.
        /// </summary>
        /// <param name="i">A coefficient index.</param>
        public Complex this[int i]
        {
            get
            {
                return _coeffs[i];
            }

            set
            {
                _coeffs[i] = value;
            }
        }

        /// <summary>
        /// Gets the degree of the complex polynomial.
        /// </summary>
        public int Degree
        {
            get
            {
                int degree = _coeffs.Length - 1;

                while (degree > 0 && _coeffs[degree] == Complex.Zero)
                    degree--;

                return degree;
            }
        }

        /// <summary>
        /// Gets the number of coefficients of the polynomial.
        /// </summary>
        public int Length
        {
            get
            {
                return _coeffs.Length;
            }
        }

        #endregion

        #region Constructors

        private CPolynomial()
        {
        }

        /// <summary>
        /// Initializes a complex polynomial with the specified coefficients count.
        /// </summary>
        /// <param name="length">The number of coefficients.</param>
        public CPolynomial(int length)
        {
            _coeffs = new Complex[length];
        }

        /// <summary>
        /// Initializes a complex polynomial from another instance of a complex polynomial.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        public CPolynomial(CPolynomial poly)
        {
            _coeffs = (Complex[])poly._coeffs.Clone();
        }

        /// <summary>
        /// Initializes a complex polynomial from a real array.
        /// </summary>
        /// <param name="data">One-dimensional real array.</param>
        public CPolynomial(IEnumerable<double> data)
        {
            _coeffs = data.Select(x => (Complex)x).ToArray();
        }

        /// <summary>
        /// Initializes a complex polynomial from a complex array.
        /// </summary>
        /// <param name="data">One-dimensional complex array.</param>
        public CPolynomial(IEnumerable<Complex> data)
        {
            _coeffs = data.ToArray();
        }

        #endregion

        #region Methods

        #region Statics

        /// <summary>
        /// Returns a complex polynomial that has the specified roots.
        /// </summary>
        /// <param name="roots">An array of complex roots.</param>
        /// <returns>The complex polynomial with the specified roots.</returns>
        public static CPolynomial FromRoots(IList<Complex> roots)
        {
            int n = roots.Count;
            CPolynomial result = new CPolynomial(n + 1);
            result[n] = 1.0;

            for (int i = 0; i < n; i++)
            {
                for (int j = n - i - 1; j < n; j++)
                {
                    result[j] = result[j] - roots[i] * result[j + 1];
                }
            }

            return result;
        }

        /// <summary>
        /// Adds two complex polynomials.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>The sum of complex polynomials.</returns>
        public static CPolynomial Add(CPolynomial poly1, CPolynomial poly2)
        {
            int len1 = poly1.Degree + 1;
            int len2 = poly2.Degree + 1;

            int maxLen = Math.Max(len1, len2);
            int minLen = Math.Min(len1, len2);

            CPolynomial result = new CPolynomial(maxLen);

            for (int i = 0; i < minLen; i++)
            {
                result[i] = poly1[i] + poly2[i];
            }

            if (len1 == maxLen)
            {
                for (int i = minLen; i < maxLen; i++)
                    result[i] = poly1[i];
            }
            else
            {
                for (int i = minLen; i < maxLen; i++)
                    result[i] = poly2[i];
            }

            return result;
        }

        /// <summary>
        /// Subtracts one complex polynomial from another.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>The result of subtracting poly2 from poly1.</returns>
        public static CPolynomial Subtract(CPolynomial poly1, CPolynomial poly2)
        {
            int len1 = poly1.Degree + 1;
            int len2 = poly2.Degree + 1;

            int maxLen = Math.Max(len1, len2);
            int minLen = Math.Min(len1, len2);

            CPolynomial result = new CPolynomial(maxLen);

            for (int i = 0; i < minLen; i++)
            {
                result[i] = poly1[i] - poly2[i];
            }

            if (len1 == maxLen)
            {
                for (int i = minLen; i < maxLen; i++)
                    result[i] = poly1[i];
            }
            else
            {
                for (int i = minLen; i < maxLen; i++)
                    result[i] = -poly2[i];
            }

            return result;
        }

        /// <summary>
        /// Multiplies two complex polynomials.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>The product of poly1 and poly2.</returns>
        public static CPolynomial Multiply(CPolynomial poly1, CPolynomial poly2)
        {
            int len1 = poly1.Degree + 1;
            int len2 = poly2.Degree + 1;

            CPolynomial result = new CPolynomial(len1 + len2 - 1);

            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    result[i + j] += poly1[i] * poly2[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies a complex polynomial by a scalar.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <param name="number">A complex number.</param>
        /// <returns>The product of poly and number.</returns>
        public static CPolynomial Multiply(CPolynomial poly, Complex number)
        {
            CPolynomial result = new CPolynomial(poly.Degree + 1);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = poly[i] * number;
            }

            return result;
        }

        /// <summary>
        /// Divides two complex polynomials.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>Quotient of division of poly1 on poly2.</returns>
        /// <exception cref="System.DivideByZeroException">The polynomial poly2 is zero.</exception>
        public static CPolynomial Divide(CPolynomial poly1, CPolynomial poly2)
        {
            CPolynomial remainder;
            return Divide(poly1, poly2, out remainder);
        }

        /// <summary>
        /// Divides two complex polynomials.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <param name="remainder">Remainder of division of the first polynomial in the second.</param>
        /// <returns>Quotient of division of poly1 on poly2.</returns>
        /// <exception cref="System.DivideByZeroException">The polynomial poly2 is zero.</exception>
        public static CPolynomial Divide(CPolynomial poly1, CPolynomial poly2, out CPolynomial remainder)
        {
            int len1 = poly1.Degree + 1;
            int len2 = poly2.Degree + 1;

            CPolynomial q = new CPolynomial(len1 - len2 + 1);
            CPolynomial r = Clean(poly1);
            Complex a = poly2[len2 - 1];

            for (int i = q.Length - 1; i >= 0; i--)
            {
                q[i] = r[r.Length - 1] / a;

                CPolynomial rt = new CPolynomial(r.Length - 1);

                for (int j = rt.Length - 1, k = len2 - 2; j > rt.Length - len2; j--, k--)
                    rt[j] = r[j] - q[i] * poly2[k];

                for (int j = rt.Length - len2; j >= 0; j--)
                    rt[j] = r[j];

                r = rt;
            }

            remainder = r;
            return q;
        }

        /// <summary>
        /// Divides a complex polynomial by a scalar.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <param name="number">A complex number.</param>
        /// <returns>The result of dividing poly by number.</returns>
        /// <exception cref="System.DivideByZeroException">The number is zero.</exception>
        public static CPolynomial Divide(CPolynomial poly, Complex number)
        {
            CPolynomial result = new CPolynomial(poly.Degree + 1);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = poly[i] / number;
            }

            return result;
        }

        /// <summary>
        /// Divides a complex polynomial by a binomial of the form (x - c).
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <param name="c">A binomial coefficient in the constant term.</param>
        /// <param name="remainder">Remainder of division of the polynomial in the binomial.</param>
        /// <returns>Quotient of division of poly on the binomial (x - c).</returns>
        public static CPolynomial DivBinom(CPolynomial poly, Complex c, out Complex remainder)
        {
            int len = poly.Degree + 1;
            CPolynomial result = new CPolynomial(len - 1);
            remainder = poly[len - 1];

            for (int i = result.Length - 1; i >= 0; i--)
            {
                result[i] = remainder;
                remainder = remainder * c + poly[i];
            }

            return result;
        }

        /// <summary>
        /// Returns the remainder of dividing the first polynomial in the second.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>The remainder of the division poly1 to poly2.</returns>
        public static CPolynomial Modulus(CPolynomial poly1, CPolynomial poly2)
        {
            CPolynomial remainder;
            Divide(poly1, poly2, out remainder);
            return remainder;
        }

        /// <summary>
        /// Returns the result of multiplying the complex polynomial by negative one.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <returns>The result of poly multiplied by negative one.</returns>
        public static CPolynomial Negate(CPolynomial poly)
        {
            CPolynomial result = new CPolynomial(poly.Degree + 1);

            for (int i = 0; i < result.Length; i++)
                result[i] = -poly[i];

            return result;
        }

        /// <summary>
        /// Returns a vector of approximate values of roots of a complex polynomial by Laguerre's method.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <returns>The approximate values of roots of poly.</returns>
        /// <exception cref="System.ArgumentException">
        /// Number of elements in poly is less than 2 or more than 99.
        /// </exception>
        public static Complex[] LaguerreRoots(CPolynomial poly)
        {
            const int maxIters = 100;
            const int maxAttempts = 10;

            // Remove zero elements standing at the end.
            int lidx = 0;
            int ridx = poly.Length - 1;
            while (ridx >= 0 && poly[ridx] == Complex.Zero) ridx--;
            while (lidx < poly.Length && poly[lidx] == Complex.Zero) lidx++;
            int length = ridx + 1;

            if (length < 2 || length > 99)
                throw new ArgumentException("Number of coefficients must be between 1 and 100.");

            int rootsCount = length - 1;
            Complex[] roots = new Complex[rootsCount];

            CPolynomial div = new CPolynomial(ridx - lidx + 1);
            for (int i = lidx; i <= ridx; i++)
                div[i - lidx] = poly[i];

            Random rand = new Random(0);

            // Main loop
            for (int i = 0; i < ridx - lidx; i++)
            {
                Complex a = rand.Next(-1000, 1000);
                if (a == 0) a = 1;
                Complex a_old = a + 1;
                int currIter = 0;
                int currAttempt = 0;

                // Degree of the div polynomial
                int n = div.Length - 1;

                while (!NumericUtil.FuzzyEquals(a, a_old, Machine.Epsilon))
                {
                    if (currIter > maxIters)
                    {
                        a = rand.Next(-1000, 1000);
                        if (a == 0) a = 1;
                        a_old = a + 1;
                        currIter = 0;
                        currAttempt++;

                        if (currAttempt > maxAttempts)
                            throw new NotConvergenceException();

                        continue;
                    }

                    currIter++;

                    Complex p = div.Evaluate(a);
                    Complex dp = div.FirstDerivative(a);
                    Complex d2p = div.SecondDerivative(a);

                    if (Complex.Abs(p) <= Machine.Epsilon)
                        break;

                    Complex G = dp / p;
                    Complex H = G * G - d2p / p;

                    Complex D = Complex.Sqrt((n - 1) * (n * H - G * G));
                    Complex denom = Complex.Abs(G + D) >= Complex.Abs(G - D) ? G + D : G - D;
                    if (denom == Complex.Zero) denom = Machine.Epsilon;

                    a_old = a;
                    a = a - n / denom;
                }

                roots[i] = a;

                Complex rem;
                div = DivBinom(div, a, out rem);
            }
            
            Array.Sort<Complex>(roots, new ComplexComparer());
            return roots;
        }

        /// <summary>
        /// Returns the interpolating polynomial through a set of nodes using the Lagrange method.
        /// </summary>
        /// <param name="xValues">
        /// A real array containing the x-coordinates of interpolation nodes.
        /// The elements of this array must be distinct.
        /// </param>
        /// <param name="yValues">
        /// A real array containing the y-coordinates of interpolation nodes.
        /// </param>
        /// <returns>The interpolating polynomial for the specified nodes.</returns>
        /// <exception cref="System.ArgumentException">The arrays xValues and yValues have different lengths.</exception>
        public static CPolynomial InterpolatingPolynomial(IList<double> xValues, IList<double> yValues)
        {
            if (xValues.Count != yValues.Count)
                throw new ArgumentException("The arrays of abscissas and ordinates have different lengths.");

            int n = xValues.Count;

            CPolynomial L = new CPolynomial(n);

            for (int i = 0; i < n; i++)
            {
                CPolynomial l = new CPolynomial(n);
                l[n - 1] = 1;

                Complex denom = 1;

                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        int k = (j > i) ? n - j - 1 : n - j - 2;

                        for (; k < n - 1; k++)
                        {
                            l[k] = l[k] - xValues[j] * l[k + 1];
                        }

                        denom *= (xValues[i] - xValues[j]);
                    }
                }

                Complex factor = yValues[i] / denom;

                for (int j = 0; j < n; j++)
                {
                    L[j] += factor * l[j];
                }
            }

            return L;
        }

        /// <summary>
        /// Returns the complex polynomial without leading zeros.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <returns>The poly without leading zeros.</returns>
        public static CPolynomial Clean(CPolynomial poly)
        {
            CPolynomial result = new CPolynomial(poly.Degree + 1);

            for (int i = 0; i < result.Length; i++)
                result[i] = poly[i];

            return result;
        }

        /// <summary>
        /// Returns the value indicating whether two instances of complex polynomial are equal.
        /// </summary>
        /// <param name="poly1">The first complex polynomial to compare.</param>
        /// <param name="poly2">The second complex polynomial to compare.</param>
        /// <returns>True if the poly1 and poly2 parameters have the same value; otherwise, false.</returns>
        public static bool Equals(CPolynomial poly1, CPolynomial poly2)
        {
            if (poly1.Degree != poly2.Degree)
                return false;

            int len = poly1.Degree + 1;
            for (int i = 0; i < len; i++)
            {
                if (poly1[i] != poly2[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Converts string representation into the equivalent of a complex polynomial.
        /// </summary>
        /// <param name="s">A string containing a complex polynomial to convert.</param>
        /// <returns>A complex polynomial equivalent to the value specified in s.</returns>
        /// <exception cref="System.ArgumentNullException">The string s is null.</exception>
        /// <exception cref="System.ArgumentException">The string s is empty string.</exception>
        /// <exception cref="System.FormatException">The string s is not a polynomial in a valid format.</exception>
        public static CPolynomial Parse(string s)
        {
            return Parse(s, null);
        }

        /// <summary>
        /// Converts string representation of a polynomial
        /// in a specified culture-specific format to its complex polynomial equivalent.
        /// </summary>
        /// <param name="s">A string containing a complex polynomial to convert.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information about s.</param>
        /// <returns>A complex polynomial equivalent to the value specified in s.</returns>
        /// <exception cref="System.ArgumentNullException">The string s is null.</exception>
        /// <exception cref="System.ArgumentException">The string s is empty string.</exception>
        /// <exception cref="System.FormatException">The string s is not a polynomial in a valid format.</exception>
        public static CPolynomial Parse(string s, IFormatProvider provider)
        {
            if (s == null) throw new ArgumentNullException(Properties.Resources.EXC_STRING_NOT_NULL);

            string str = s.Trim();
            if (String.IsNullOrEmpty(str)) throw new ArgumentException(Properties.Resources.EXC_STRING_NOT_EMPTY);

            string varName = String.Empty;

            Match matchAll = _polyRegex.Match(str);
            if (!matchAll.Success) throw new FormatException(Properties.Resources.EXC_POLY_INCCORECT_FORMAT);

            SortedDictionary<int, Complex> p = new SortedDictionary<int, Complex>();

            int maxExp = 0;
            foreach (Capture capture in matchAll.Groups["term"].Captures)
            {
                Match match = _polyPartRegex.Match(capture.Value.Trim());

                if (!String.IsNullOrEmpty(match.Value))
                {
                    string sign_s = match.Groups["sign"].Value;
                    string coef_s = match.Groups["num"].Value;
                    string var_s = match.Groups["var"].Value;
                    string exp_s = match.Groups["exp"].Value;

                    if (!String.IsNullOrEmpty(var_s))
                    {
                        if (String.IsNullOrEmpty(varName)) varName = var_s;
                        if (varName != var_s)
                            throw new FormatException(Properties.Resources.EXC_POLY_MISMATCH_VAR_NAMES);
                    }

                    int sign = int.Parse(sign_s + 1, provider);
                    Complex coef = !String.IsNullOrEmpty(coef_s) ? sign * Complex.Parse(coef_s, provider) : sign;
                    int exp = !String.IsNullOrEmpty(exp_s) ? int.Parse(exp_s, provider) : 0;

                    if (!String.IsNullOrEmpty(var_s) && String.IsNullOrEmpty(exp_s))
                        exp = 1;

                    if (maxExp < exp) maxExp = exp;

                    if (p.ContainsKey(exp))
                        p[exp] += coef;
                    else
                        p.Add(exp, coef);
                }
            }

            // Need to find the maximum degree and put here
            CPolynomial poly = new CPolynomial(maxExp + 1);
            foreach (KeyValuePair<int, Complex> pair in p)
            {
                poly[pair.Key] = pair.Value;
            }

            return poly;
        }

        /// <summary>
        /// Converts the complex coefficient of the polynomial to its equivalent string representation.
        /// </summary>
        /// <param name="coeff">A complex coefficient.</param>
        /// <param name="varName">Represents the variable name substitution.</param>
        /// <param name="format">A numeric format string.</param>
        /// <param name="nfi">A System.Globalization.NumberFormatInfo that supplies culture-specific formatting numeric values.</param>
        /// <returns>The string representation of coeff as specified by format and nfi.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        private static string CoefficientToString(Complex coeff, string varName, string format, NumberFormatInfo nfi)
        {
            if (coeff.IsReal)
            {
                double realPart = coeff.Re;

                if (realPart == 1.0)
                    return String.Format("{0} {1}", nfi.PositiveSign, varName);
                else if (realPart == -1.0)
                    return String.Format("{0} {1}", nfi.NegativeSign, varName);
                else if (realPart >= 0)
                    return String.Format("{0} {1}*{2}", nfi.PositiveSign, realPart.ToString(format, nfi), varName);
                else
                    return String.Format("{0} {1}*{2}", nfi.NegativeSign, (-realPart).ToString(format, nfi), varName);
            }
            else if (coeff.IsImaginary)
            {
                if (coeff.Im >= 0)
                    return String.Format("{0} {1}*{2}", nfi.PositiveSign, coeff.ToString(format, nfi), varName);
                else
                    return String.Format("{0} {1}*{2}", nfi.NegativeSign, (-coeff).ToString(format, nfi), varName);
            }
            else
            {
                return String.Format("{0} ({1})*{2}", nfi.PositiveSign, coeff.ToString(format, nfi), varName);
            }
        }

        #endregion

        #region Dynamics

        /// <summary>
        /// Returns the value of the complex polynomial evaluated at a specified value.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The evaluated value of the complex polynomial.</returns>
        public Complex Evaluate(Complex value)
        {
            Complex result = Complex.Zero;

            for (int i = Length - 1; i >= 0; i--)
            {
                result = result * value + this[i]; 
            }

            return result;
        }

        /// <summary>
        /// Returns the first derivative of the complex polynomial.
        /// </summary>
        /// <returns>The first derivative of this instance.</returns>
        public CPolynomial FirstDerivative()
        {
            int len = Degree + 1;
            if (len <= 1) return new CPolynomial(1);
            CPolynomial result = new CPolynomial(len - 1);

            for (int i = 1; i < len; i++)
            {
                result[i - 1] = this[i] * i;
            }

            return result;
        }

        /// <summary>
        /// Returns the value of the first derivative evaluated at a specified value.
        /// </summary>
        /// <param name="value">A complex number to evaluation.</param>
        /// <returns>The evaluated value of the first derivative.</returns>
        public Complex FirstDerivative(Complex value)
        {
            Complex result = Complex.Zero;

            for (int i = Length - 1; i >= 1; i--)
            {
                result = result * value + this[i] * i;
            }

            return result;
        }

        /// <summary>
        /// Returns the second derivative of the complex polynomial.
        /// </summary>
        /// <returns>The second derivative of this instance.</returns>
        public CPolynomial SecondDerivative()
        {
            int len = Degree + 1;
            if (len <= 2) return new CPolynomial(1);
            CPolynomial result = new CPolynomial(len - 2);

            for (int i = 2; i < len; i++)
            {
                result[i - 2] = this[i] * i * (i - 1);
            }

            return result;
        }

        /// <summary>
        /// Returns the value of the second derivative evaluated at a specified value.
        /// </summary>
        /// <param name="value">A complex number to evaluation.</param>
        /// <returns>The evaluated value of the second derivative.</returns>
        public Complex SecondDerivative(Complex value)
        {
            Complex result = Complex.Zero;

            for (int i = Length - 1; i >= 2; i--)
            {
                result = result * value + this[i] * i * (i - 1);
            }

            return result;
        }

        /// <summary>
        /// Returns the n-th derivative of the complex polynomial.
        /// </summary>
        /// <param name="order">An order of derivative.</param>
        /// <returns>The n-th derivative of this instance.</returns>
        public CPolynomial NthDerivative(int order)
        {
            int len = Degree + 1;
            if (len <= order) return new CPolynomial(1);
            CPolynomial result = new CPolynomial(len - order);

            for (int i = order; i < len; i++)
            {
                int exp = 1, k = i;
                for (int j = 0; j < order; j++)
                    exp *= k--;

                result[i - order] = this[i] * exp;
            }

            return result;
        }

        /// <summary>
        /// Returns the value of the n-th derivative evaluated at a specified value.
        /// </summary>
        /// <param name="order">An order of derivative.</param>
        /// <param name="value">A complex number to evaluation.</param>
        /// <returns>The evaluated value of the n-th derivative.</returns>
        public Complex NthDerivative(int order, Complex value)
        {
            Complex result = Complex.Zero;

            for (int i = Length - 1; i >= order; i--)
            {
                int exp = 1, k = i;
                for (int j = 0; j < order; j++)
                    exp *= k--;

                result = result * value + this[i] * exp;
            }

            return result;
        }

        /// <summary>
        /// Returns the antiderivative of the complex polynomial.
        /// </summary>
        /// <returns>The antiderivative of this instance.</returns>
        public CPolynomial Antiderivative()
        {
            int len = Degree + 1;
            CPolynomial result = new CPolynomial(len + 1);

            for (int i = 1; i < result.Length; i++)
                result[i] = _coeffs[i - 1] / i;

            return result;
        }

        /// <summary>
        /// Returns the normalized complex polynomial.
        /// </summary>
        /// <returns>The normalized complex polynomial.</returns>
        public CPolynomial Normalize()
        {
            CPolynomial result = new CPolynomial(Degree + 1);
            Complex divisor = _coeffs[result.Length - 1];

            for (int i = 0; i < result.Length; i++)
                result[i] = _coeffs[i] / divisor;

            return result;
        }

        /// <summary>
        /// Removes the leading zero coefficients.
        /// </summary>
        public void Clean()
        {
            _coeffs = Clean(this)._coeffs;
        }

        /// <summary>
        /// Copies the coefficients of the complex polynomial to a new complex array.
        /// </summary>
        /// <returns>An complex array containing copies of the cofficients of the CPolynomial.</returns>
        public Complex[] ToArray()
        {
            return (Complex[])Clean(this)._coeffs.Clone();
        }

        /// <summary>
        /// Converts the complex polynomial of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString(null, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the complex polynomial of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString(null, provider);
        }

        /// <summary>
        /// Converts the complex polynomial of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format)
        {
            return ToString(format, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the complex polynomial of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <param name="varName">Represents the variable name substitution.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format, IFormatProvider provider, string varName)
        {
            NumberFormatInfo nfi = NumberFormatInfo.GetInstance(provider);
            StringBuilder sb = new StringBuilder();

            if (_coeffs[0] != Complex.Zero)
                sb.AppendFormat("{0} ", _coeffs[0].ToString(format, nfi));

            if (Length > 1 && _coeffs[1] != Complex.Zero)
                sb.AppendFormat("{0} ", CoefficientToString(_coeffs[1], varName, format, nfi));

            for (int i = 2; i < Length; i++)
            {
                if (_coeffs[i] != Complex.Zero)
                    sb.AppendFormat("{0}^{1} ", CoefficientToString(_coeffs[i], varName, format, nfi), i);
            }

            if (sb.Length > 0)
            {
                if (sb[0] == nfi.PositiveSign[0])
                {
                    int len = sb.Length - nfi.PositiveSign.Length - 1;
                    return sb.ToString(nfi.PositiveSign.Length + 1, len);
                }
                else if (sb[0] == nfi.NegativeSign[0] && sb[nfi.NegativeSign.Length] == ' ')
                {
                    sb.Remove(nfi.NegativeSign.Length, 1);
                    return sb.ToString(0, sb.Length - 1);
                }
                else
                {
                    return sb.ToString(0, sb.Length - 1);
                }
            }
            else
            {
                return nfi.NativeDigits[0];
            }
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// True if obj is an instance of CPolynomial and equals the value of this instance;
        /// otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is CPolynomial))
                return false;

            return Equals(this, (CPolynomial)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified
        /// complex polynomial.
        /// </summary>
        /// <param name="obj">A CPolynomial object to compare to this instance.</param>
        /// <returns>True if obj is equal to this instance; otherwise, false.</returns>
        public bool Equals(CPolynomial obj)
        {
            return Equals(this, obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = 0;
            int len = Degree + 1;

            for (int i = 0; i < len; i++)
            {
                hashCode ^= this[i].GetHashCode() >> 3;
            }

            return hashCode;
        }

        #endregion

        #endregion

        #region Operators

        /// <summary>
        /// Returns a value indicating whether
        /// two instances of complex polynomial are equal.
        /// </summary>
        /// <param name="poly1">The first complex polynomial to compare.</param>
        /// <param name="poly2">The second complex polynomial to compare.</param>
        /// <returns>True if the poly1 and poly2 parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(CPolynomial poly1, CPolynomial poly2)
        {
            return Equals(poly1, poly2);
        }

        /// <summary>
        /// Returns a value indicating whether
        /// two instances of complex polynomial are not equal.
        /// </summary>
        /// <param name="poly1">The first complex polynomial to compare.</param>
        /// <param name="poly2">The second complex polynomial to compare.</param>
        /// <returns>True if poly1 and poly2 are not equal; otherwise, false.</returns>
        public static bool operator !=(CPolynomial poly1, CPolynomial poly2)
        {
            return !Equals(poly1, poly2);
        }

        /// <summary>
        /// Returns the value of the complex polynomial operand
        /// (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <returns>The value of the operand, poly.</returns>
        public static CPolynomial operator +(CPolynomial poly)
        {
            return new CPolynomial(poly);
        }

        /// <summary>
        /// Negates the value of the complex polynomial operand.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <returns>The result of poly multiplied by negative one.</returns>
        public static CPolynomial operator -(CPolynomial poly)
        {
            return Negate(poly);
        }

        /// <summary>
        /// Adds two complex polynomials.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>The sum of complex polynomials.</returns>
        public static CPolynomial operator +(CPolynomial poly1, CPolynomial poly2)
        {
            return Add(poly1, poly2);
        }

        /// <summary>
        /// Subtracts one complex polynomial from another.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>The result of subtracting poly2 from poly1.</returns>
        public static CPolynomial operator -(CPolynomial poly1, CPolynomial poly2)
        {
            return Subtract(poly1, poly2);
        }

        /// <summary>
        /// Multiplies two complex polynomials.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>The product of poly1 and poly2.</returns>
        public static CPolynomial operator *(CPolynomial poly1, CPolynomial poly2)
        {
            return Multiply(poly1, poly2);
        }

        /// <summary>
        /// Multiplies a complex polynomial by a scalar.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <param name="number">A complex number.</param>
        /// <returns>The product of poly and number.</returns>
        public static CPolynomial operator *(CPolynomial poly, Complex number)
        {
            return Multiply(poly, number);
        }

        /// <summary>
        /// Multiplies a scalar by a complex polynomial.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <param name="poly">A complex polynomial.</param>
        /// <returns>The product of number and poly.</returns>
        public static CPolynomial operator *(Complex number, CPolynomial poly)
        {
            return Multiply(poly, number);
        }

        /// <summary>
        /// Divides two complex polynomials.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>Quotient of division of poly1 on poly2.</returns>
        public static CPolynomial operator /(CPolynomial poly1, CPolynomial poly2)
        {
            return Divide(poly1, poly2);
        }

        /// <summary>
        /// Divides a complex polynomial by a scalar.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <param name="number">A complex number.</param>
        /// <returns>The result of dividing poly by number.</returns>
        public static CPolynomial operator /(CPolynomial poly, Complex number)
        {
            return Divide(poly, number);
        }

        /// <summary>
        /// Returns the remainder of dividing the first polynomial in the second.
        /// </summary>
        /// <param name="poly1">The first complex polynomial.</param>
        /// <param name="poly2">The second complex polynomial.</param>
        /// <returns>The remainder of the division poly1 to poly2.</returns>
        public static CPolynomial operator %(CPolynomial poly1, CPolynomial poly2)
        {
            return Modulus(poly1, poly2);
        }

        #endregion


        #region ICloneable Members

        /// <summary>
        /// Creates a shallow copy of the complex polynomial.
        /// </summary>
        /// <returns>A shallow copy of the CPolynomial.</returns>
        public Object Clone()
        {
            return new CPolynomial(_coeffs);
        }

        #endregion

        #region IFormattable Members

        /// <summary>
        /// Converts the complex polynomial of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format, IFormatProvider provider)
        {
            return ToString(format, provider, "x");
        }

        #endregion

        #region IXmlSerializable Members

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            string value = reader.ReadElementString();
            CPolynomial poly = Parse(value, CultureInfo.InvariantCulture);
            _coeffs = poly._coeffs;
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteValue(ToString(null, CultureInfo.InvariantCulture, "x").Replace(" ", String.Empty));
        }

        #endregion
    }
}
