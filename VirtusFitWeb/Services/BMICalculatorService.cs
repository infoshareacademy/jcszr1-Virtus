using System;

namespace VirtusFitWeb.Services
{
    public class BMICalculatorService: IBMICalculatorService
    {
        public double CalculateBMI(double height, double weight)
        {
            return weight / (height / 100 * height / 100);
            
        }
    }
}
