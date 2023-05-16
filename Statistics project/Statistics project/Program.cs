namespace Statistics_project
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            // Read input
            Console.Write("Enter the number of items: ");
            int n = int.Parse(Console.ReadLine());
            double[] values = new double[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Enter value {i + 1}: ");
                values[i] = double.Parse(Console.ReadLine());
            }

            // Sort values
            Array.Sort(values);

            // Calculate statistics
            double median = CalculateMedian(values);
            double mode = CalculateMode(values);
            double range = CalculateRange(values);
            double q1 = CalculateQuartile(values, 0.25);
            double q3 = CalculateQuartile(values, 0.75);
            double p90 = CalculatePercentile(values, 0.9);
            double iqr = q3 - q1;
            double lowerBound = q1 - 1.5 * iqr;
            double upperBound = q3 + 1.5 * iqr;

            // Print results
            Console.WriteLine($"Median: {median}");
            Console.WriteLine($"Mode: {mode}");
            Console.WriteLine($"Range: {range}");
            Console.WriteLine($"1st Quartile: {q1}");
            Console.WriteLine($"3rd Quartile: {q3}");
            Console.WriteLine($"P90: {p90}");
            Console.WriteLine($"Interquartile Range: {iqr}");
            Console.WriteLine($"Outlier boundaries: {lowerBound} - {upperBound}");

            // Check for outliers
            Console.Write("Enter a value to check for outliers: ");
            double input = double.Parse(Console.ReadLine());
            if (IsOutlier(input, lowerBound, upperBound))
            {
                Console.WriteLine($"{input} is an outlier");
            }
            else
            {
                Console.WriteLine($"{input} is not an outlier");
            }

            Console.ReadLine();
        }

        static double CalculateMedian(double[] values)
        {
            int n = values.Length;
            if (n % 2 == 0)
            {
                return (values[n / 2 - 1] + values[n / 2]) / 2;
            }
            else
            {
                return values[n / 2];
            }
        }

        static double CalculateMode(double[] values)
        {
            var groups = values.GroupBy(x => x);
            int maxCount = groups.Max(g => g.Count());
            return groups.First(g => g.Count() == maxCount).Key;
        }

        static double CalculateRange(double[] values)
        {
            return values.Last() - values.First();
        }

        static double CalculateQuartile(double[] values, double percentile)
        {
            int n = values.Length;
            double index = (n - 1) * percentile + 1;
            if (index % 1 == 0)
            {
                return values[(int)index - 1];
            }
            else
            {
                int k = (int)index;
                double d = index % 1;
                return values[k - 1] + d * (values[k] - values[k - 1]);
            }
        }

        static double CalculatePercentile(double[] values, double percentile)
        {
            int n = values.Length;
            double index = (n - 1) * percentile + 1;
            if (index % 1 == 0)
            {
                return values[(int)index - 1];
            }
            else
            {
                int k = (int)index;
                double d = index % 1;
                return values[k - 1] + d * (values[k] - values[k - 1]);
            }
        }

        static bool IsOutlier(double value, double lowerBound, double upperBound)
        {
            return value < lowerBound || value > upperBound;
        }
    }
}

