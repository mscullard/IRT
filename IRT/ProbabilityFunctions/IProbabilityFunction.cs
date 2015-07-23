namespace IRT.ProbabilityFunctions
{
    // Interface for functions which supply the probability of obtaining a correct score on an item given the person's location
    public interface IProbabilityFunction
    {
        double ProbabilityOfCorrectResponse(double theta);
        double FirstThetaDerivative(double theta);
        double SecondThetaDerivative(double theta);
    }
}