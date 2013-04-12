using System;


namespace TAlex.MathCore.NumericalAnalysis.NumericalIntegration
{
    /// <summary>
    /// Represents the Gauss–Kronrod quadrature formulas.
    /// </summary>
    public static class GaussKronrodQuadratures
    {
        #region Fields

        private static double[] XGK15 = {
            0.99145537112081263921, 0.94910791234275852453, 0.86486442335976907279,
            0.74153118559939443986, 0.58608723546769113029, 0.40584515137739716691,
            0.20778495500789846760, 0.00000000000000000000};

        private static double[] WGK15 = {
            0.02293532201052922496, 0.06309209262997855329, 0.10479001032225018384,
            0.14065325971552591875, 0.16900472663926790283, 0.19035057806478540991,
            0.20443294007529889241, 0.20948214108472782801};

        private static double[] WG7 = {
            0.12948496616886969327, 0.27970539148927666790, 0.38183005050511894495,
            0.41795918367346938776};



        private static double[] XGK21 = {
            0.99565716302580808074, 0.97390652851717172008, 0.93015749135570822600,
            0.86506336668898451073, 0.78081772658641689706, 0.67940956829902440623,
            0.56275713466860468334, 0.43339539412924719080, 0.29439286270146019813,
            0.14887433898163121088, 0.00000000000000000000};

        private static double[] WGK21 = {
            0.01169463886737187428, 0.03255816230796472748, 0.05475589657435199603,
            0.07503967481091995277, 0.09312545458369760554, 0.10938715880229764190,
            0.12349197626206585108, 0.13470921731147332593, 0.14277593857706008080,
            0.14773910490133849137, 0.14944555400291690566};

        private static double[] WG10 = {
            0.06667134430868813759, 0.14945134915058059315, 0.21908636251598204400,
            0.26926671930999635509, 0.29552422471475287017};



        private static double[] XGK31 = {
            0.99800229869339706029, 0.98799251802048542849, 0.96773907567913913426,
            0.93727339240070590431, 0.89726453234408190088, 0.84820658341042721620,
            0.79041850144246593297, 0.72441773136017004742, 0.65099674129741697053,
            0.57097217260853884754, 0.48508186364023968069, 0.39415134707756336990,
            0.29918000715316881217, 0.20119409399743452230, 0.10114206691871749903,
            0.00000000000000000000};

        private static double[] WGK31 = {
            0.00537747987292334899, 0.01500794732931612254, 0.02546084732671532019,
            0.03534636079137584622, 0.04458975132476487661, 0.05348152469092808727,
            0.06200956780067064029, 0.06985412131872825871, 0.07684968075772037889,
            0.08308050282313302104, 0.08856444305621177065, 0.09312659817082532123,
            0.09664272698362367851, 0.09917359872179195933, 0.10076984552387559504,
            0.10133000701479154902};

        private static double[] WG15 = {
            0.03075324199611726835, 0.07036604748810812471, 0.10715922046717193501,
            0.13957067792615431445, 0.16626920581699393355, 0.18616100001556221103,
            0.19843148532711157646, 0.20257824192556127288};



        private static double[] XGK41 = {
            0.99885903158827766384, 0.99312859918509492479, 0.98150787745025025919,
            0.96397192727791379127, 0.94082263383175475352, 0.91223442825132590587,
            0.87827681125228197608, 0.83911697182221882339, 0.79504142883755119835,
            0.74633190646015079261, 0.69323765633475138481, 0.63605368072651502545,
            0.57514044681971031534, 0.51086700195082709800, 0.44359317523872510320,
            0.37370608871541956067, 0.30162786811491300432, 0.22778585114164507808,
            0.15260546524092267551, 0.07652652113349733375, 0.00000000000000000000};

        private static double[] WGK41 = {
            0.00307358371852053150, 0.00860026985564294220, 0.01462616925697125298,
            0.02038837346126652360, 0.02588213360495115883, 0.03128730677703279896,
            0.03660016975820079803, 0.04166887332797368626, 0.04643482186749767472,
            0.05094457392372869193, 0.05519510534828599474, 0.05911140088063957237,
            0.06265323755478116803, 0.06583459713361842211, 0.06864867292852161935,
            0.07105442355344406831, 0.07303069033278666750, 0.07458287540049918899,
            0.07570449768455667466, 0.07637786767208073671, 0.07660071191799965645};

        private static double[] WG20 = {
            0.01761400713915211831, 0.04060142980038694133, 0.06267204833410906357,
            0.08327674157670474872, 0.10193011981724043504, 0.11819453196151841731,
            0.13168863844917662690, 0.14209610931838205133, 0.14917298647260374679,
            0.15275338713072585070};



        private static double[] XGK51 = {
            0.99926210499260983419, 0.99555696979049809791, 0.98803579453407724764,
            0.97666392145951751150, 0.96161498642584251242, 0.94297457122897433941,
            0.92074711528170156175, 0.89499199787827536885, 0.86584706529327559545,
            0.83344262876083400142, 0.79787379799850005941, 0.75925926303735763058,
            0.71776640681308438819, 0.67356636847346836449, 0.62681009901031741279,
            0.57766293024122296772, 0.52632528433471918260, 0.47300273144571496052,
            0.41788538219303774885, 0.36117230580938783774, 0.30308953893110783017,
            0.24386688372098843205, 0.18371893942104889202, 0.12286469261071039639,
            0.06154448300568507889, 0.00000000000000000000};

        private static double[] WGK51 = {
            0.00198738389233031593, 0.00556193213535671376, 0.00947397338617415161,
            0.01323622919557167481, 0.01684781770912829823, 0.02043537114588283546,
            0.02400994560695321622, 0.02747531758785173780, 0.03079230016738748889,
            0.03400213027432933784, 0.03711627148341554356, 0.04008382550403238207,
            0.04287284502017004948, 0.04550291304992178891, 0.04798253713883671391,
            0.05027767908071567196, 0.05236288580640747586, 0.05425112988854549014,
            0.05595081122041231731, 0.05743711636156783285, 0.05868968002239420796,
            0.05972034032417405998, 0.06053945537604586295, 0.06112850971705304831,
            0.06147118987142531666, 0.06158081806783293508};

        private static double[] WG25 = {
            0.01139379850102628795, 0.02635498661503213726, 0.04093915670130631266,
            0.05490469597583519193, 0.06803833381235691721, 0.08014070033500101801,
            0.09102826198296364981, 0.10053594906705064420, 0.10851962447426365312,
            0.11485825914571164834, 0.11945576353578477223, 0.12224244299031004169,
            0.12317605372671545120};



        private static double[] XGK61 = {
            0.99948441005049063757, 0.99689348407464954027, 0.99163099687040459486,
            0.98366812327974720997, 0.97311632250112626837, 0.96002186496830751222,
            0.94437444474855997942, 0.92620004742927432588, 0.90557330769990779855,
            0.88256053579205268154, 0.85720523354606109896, 0.82956576238276839744,
            0.79972783582183908301, 0.76777743210482619492, 0.73379006245322680473,
            0.69785049479331579693, 0.66006106412662696137, 0.62052618298924286114,
            0.57934523582636169176, 0.53662414814201989926, 0.49248046786177857499,
            0.44703376953808917678, 0.40040125483039439254, 0.35270472553087811347,
            0.30407320227362507737, 0.25463692616788984644, 0.20452511668230989144,
            0.15386991360858354696, 0.10280693796673703015, 0.05147184255531769583,
            0.00000000000000000000};

        private static double[] WGK61 = {
            0.00138901369867700762, 0.00389046112709988405, 0.00663070391593129217,
            0.00927327965951776343, 0.01182301525349634174, 0.01436972950704580481,
            0.01692088918905327263, 0.01941414119394238117, 0.02182803582160919230,
            0.02419116207808060137, 0.02650995488233310161, 0.02875404876504129284,
            0.03090725756238776247, 0.03298144705748372603, 0.03497933802806002414,
            0.03688236465182122922, 0.03867894562472759295, 0.04037453895153595911,
            0.04196981021516424615, 0.04345253970135606932, 0.04481480013316266319,
            0.04605923827100698812, 0.04718554656929915395, 0.04818586175708712914,
            0.04905543455502977889, 0.04979568342707420636, 0.05040592140278234684,
            0.05088179589874960649, 0.05122154784925877217, 0.05142612853745902593,
            0.05149472942945156756};

        private static double[] WG30 = {
            0.00796819249616660562, 0.01846646831109095914, 0.02878470788332336935,
            0.03879919256962704960, 0.04840267283059405290, 0.05749315621761906648,
            0.06597422988218049513, 0.07375597473770520627, 0.08075589522942021535,
            0.08689978720108297980, 0.09212252223778612872, 0.09636873717464425964,
            0.09959342058679526706, 0.10176238974840550460, 0.10285265289355884034};

        #endregion

        #region Methods

        /// <summary>
        /// Returns the numerical value of the definite integral using the Gauss-Kronrod rule 15.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex GaussKronrod15Rule(Func<Complex, Complex> integrand, double lowerBound, double upperBound)
        {
            Complex result = Complex.Zero;

            double centre = 0.5 * (lowerBound + upperBound);
            double hlgth = 0.5 * (upperBound - lowerBound);

            for (int i = 0; i < 7; i++)
            {
                double abscissa = hlgth * XGK15[i];
                Complex f1 = integrand(centre - abscissa);
                Complex f2 = integrand(centre + abscissa);

                result += WGK15[i] * f1;
                result += WGK15[i] * f2;
            }

            result += WGK15[7] * (integrand(centre));

            return hlgth * result;
        }

        /// <summary>
        /// Returns the numerical value of the definite integral using the Gauss-Kronrod rule 21.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex GaussKronrod21Rule(Func<Complex, Complex> integrand, double lowerBound, double upperBound)
        {
            Complex result = Complex.Zero;

            double centre = 0.5 * (lowerBound + upperBound);
            double hlgth = 0.5 * (upperBound - lowerBound);

            for (int i = 0; i < 10; i++)
            {
                double abscissa = hlgth * XGK21[i];
                Complex f1 = integrand(centre - abscissa);
                Complex f2 = integrand(centre + abscissa);

                result += WGK21[i] * f1;
                result += WGK21[i] * f2;
            }

            result += WGK21[10] * (integrand(centre));

            return hlgth * result;
        }

        /// <summary>
        /// Returns the numerical value of the definite integral using the Gauss-Kronrod rule 31.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex GaussKronrod31Rule(Func<Complex, Complex> integrand, double lowerBound, double upperBound)
        {
            Complex result = Complex.Zero;

            double centre = 0.5 * (lowerBound + upperBound);
            double hlgth = 0.5 * (upperBound - lowerBound);

            for (int i = 0; i < 15; i++)
            {
                double abscissa = hlgth * XGK31[i];
                Complex f1 = integrand(centre - abscissa);
                Complex f2 = integrand(centre + abscissa);

                result += WGK31[i] * f1;
                result += WGK31[i] * f2;
            }

            result += WGK31[15] * (integrand(centre));

            return hlgth * result;
        }

        /// <summary>
        /// Returns the numerical value of the definite integral using the Gauss-Kronrod rule 41.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex GaussKronrod41Rule(Func<Complex, Complex> integrand, double lowerBound, double upperBound)
        {
            Complex result = Complex.Zero;

            double centre = 0.5 * (lowerBound + upperBound);
            double hlgth = 0.5 * (upperBound - lowerBound);

            for (int i = 0; i < 20; i++)
            {
                double abscissa = hlgth * XGK41[i];
                Complex f1 = integrand(centre - abscissa);
                Complex f2 = integrand(centre + abscissa);

                result += WGK41[i] * f1;
                result += WGK41[i] * f2;
            }

            result += WGK41[20] * (integrand(centre));

            return hlgth * result;
        }

        /// <summary>
        /// Returns the numerical value of the definite integral using the Gauss-Kronrod rule 51.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex GaussKronrod51Rule(Func<Complex, Complex> integrand, double lowerBound, double upperBound)
        {
            Complex result = Complex.Zero;

            double centre = 0.5 * (lowerBound + upperBound);
            double hlgth = 0.5 * (upperBound - lowerBound);

            for (int i = 0; i < 25; i++)
            {
                double abscissa = hlgth * XGK51[i];
                Complex f1 = integrand(centre - abscissa);
                Complex f2 = integrand(centre + abscissa);

                result += WGK51[i] * f1;
                result += WGK51[i] * f2;
            }

            result += WGK51[25] * (integrand(centre));

            return hlgth * result;
        }

        /// <summary>
        /// Returns the numerical value of the definite integral using the Gauss-Kronrod rule 61.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex GaussKronrod61Rule(Func<Complex, Complex> integrand, double lowerBound, double upperBound)
        {
            Complex result = Complex.Zero;

            double centre = 0.5 * (lowerBound + upperBound);
            double hlgth = 0.5 * (upperBound - lowerBound);

            for (int i = 0; i < 30; i++)
            {
                double abscissa = hlgth * XGK61[i];
                Complex f1 = integrand(centre - abscissa);
                Complex f2 = integrand(centre + abscissa);

                result += WGK61[i] * f1;
                result += WGK61[i] * f2;
            }

            result += WGK61[30] * (integrand(centre));

            return hlgth * result;
        }

        #endregion
    }
}
