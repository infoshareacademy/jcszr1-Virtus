using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitWeb.Services
{
    public interface IBMICalculatorService
    {
        double CalculateBMI(double height, double weight);

        void DetermineBMIStatus(double BMI);
    }
}
