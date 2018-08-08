using System;
namespace ChiSquare
{
    class ChiSquareProgram
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("\nBegin Chi-square test using C# demo\n");
                Console.WriteLine("Goal is to see if one die from a set of dice is biased or not\n");

                int[] observed = new int[] { 20, 28, 12, 32, 22, 36 };

                Console.WriteLine("\nStarting chi-square test");
                double p = ChiSquareTest(observed);
                Console.WriteLine("\nChi-square test complete");

                double crit = 0.05;
                if (p < crit)
                {
                    Console.WriteLine("\nBecause p-value is below critical value of " + crit.ToString("F2"));
                    Console.WriteLine("the null hypothsis is rejected and we conclude");
                    Console.WriteLine("the data is unlikely to have happened by chance.");
                }
                else
                {
                    Console.WriteLine("\nBecause p-value is not below critical value of " + crit.ToString("F2"));
                    Console.WriteLine("the null hypothsis is accepted (not rejected) and we conclude");
                    Console.WriteLine("the observed data could have happened by chance.");
                }

                Console.WriteLine("\nEnd\n");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        } // Main

        static void ShowVector(int[] v)
        {
            for (int i = 0; i < v.Length; ++i)
                Console.Write(v[i] + " ");
            Console.WriteLine("");
        }

        static double ChiSquareTest(int[] observed)
        {
            Console.WriteLine("Observed frequencies are: ");
            ShowVector(observed);

            double x = ChiSquareStatistic(observed);
            Console.WriteLine("\nX-squared = " + x.ToString("F2"));

            int df = observed.Length - 1;
            Console.WriteLine("\ndf = " + df);

            double p = ChiSquareProb(x, df);
            Console.WriteLine("\np-value = " + p.ToString("F6"));

            return p;
        }

        static double ChiSquareStatistic(int[] observed)
        {
            double sumObs = 0.0;
            for (int i = 0; i < observed.Length; ++i)
                sumObs += observed[i];
            double expected = (int)(sumObs / observed.Length);

            double result = 0.0;
            for (int i = 0; i < observed.Length; ++i)
            {
                result += ((observed[i] - expected) * (observed[i] - expected)) / expected;
            }
            return result;
        }

        public static double ChiSquareProb(double x, int df)
        {
            // x = a computed chi-square value. df = degrees of freedom.
            // output = prob. the x value occurred by chance.
            // so, for example, if result < 0.05 there is only a 5% chance
            // that the x value ocurred by chance and therefore 
            // we conclude that the actual data which produced x is
            // NOT the same as the expected data.
            // this function can be used to create a ChiSquareTest procedure.
            // ACM Algorithm 299 and update ACM TOMS June 1985.
            // Uses custom Exp() function below.

            if (x <= 0.0 || df < 1)
                throw new Exception("parameter x must be positive " +
                "and parameter df must be 1 or greater in ChiSquaredProb()");

            double a = 0.0; // 299 variable names
            double y = 0.0;
            double s = 0.0;
            double z = 0.0;
            double e = 0.0;
            double c;

            bool even; // is df even?

            a = 0.5 * x;
            if (df % 2 == 0) even = true; else even = false;

            if (df > 1) y = Exp(-a); // ACM update remark (4) 

            if (even == true) s = y; else s = 2.0 * Gauss(-Math.Sqrt(x));

            if (df > 2)
            {
                x = 0.5 * (df - 1.0);
                if (even == true) z = 1.0; else z = 0.5;
                if (a > 40.0) // ACM remark (5)
                {
                    if (even == true) e = 0.0; else e = 0.5723649429247000870717135; // log(sqrt(pi))
                    c = Math.Log(a); // log base e
                    while (z <= x)
                    {
                        e = Math.Log(z) + e;
                        s = s + Exp(c * z - a - e); // ACM update remark (6)
                        z = z + 1.0;
                    }
                    return s;
                } // a > 40.0
                else
                {
                    if (even == true) e = 1.0; else e = 0.5641895835477562869480795 / Math.Sqrt(a); // (1 / sqrt(pi))
                    c = 0.0;
                    while (z <= x)
                    {
                        e = e * (a / z); // ACM update remark (7)
                        c = c + e;
                        z = z + 1.0;
                    }
                    return c * y + s;
                }
            } // df > 2
            else
            {
                return s;
            }
        } // ChiSquare()

        private static double Exp(double x) // ACM update remark (3)
        {
            if (x < -40.0) // 40.0 is a magic number. ACM update remark (8)
                return 0.0;
            else
                return Math.Exp(x);
        }

        public static double Gauss(double z)
        {
            // input = z-value (-inf to +inf)
            // output = p under Normal curve from -inf to z
            // e.g., if z = 0.0, function returns 0.5000
            // ACM Algorithm #209
            double y; // 209 scratch variable
            double p; // result. called 'z' in 209
            double w; // 209 scratch variable

            if (z == 0.0)
                p = 0.0;
            else
            {
                y = Math.Abs(z) / 2;
                if (y >= 3.0)
                {
                    p = 1.0;
                }
                else if (y < 1.0)
                {
                    w = y * y;
                    p = ((((((((0.000124818987 * w
                      - 0.001075204047) * w + 0.005198775019) * w
                      - 0.019198292004) * w + 0.059054035642) * w
                      - 0.151968751364) * w + 0.319152932694) * w
                      - 0.531923007300) * w + 0.797884560593) * y * 2.0;
                }
                else
                {
                    y = y - 2.0;
                    p = (((((((((((((-0.000045255659 * y
                      + 0.000152529290) * y - 0.000019538132) * y
                      - 0.000676904986) * y + 0.001390604284) * y
                      - 0.000794620820) * y - 0.002034254874) * y
                      + 0.006549791214) * y - 0.010557625006) * y
                      + 0.011630447319) * y - 0.009279453341) * y
                      + 0.005353579108) * y - 0.002141268741) * y
                      + 0.000535310849) * y + 0.999936657524;
                }
            }

            if (z > 0.0)
                return (p + 1.0) / 2;
            else
                return (1.0 - p) / 2;
        } // Gauss()

    } // Program class

} // ns
