using System.Collections.Generic;
using System.Linq;
using IRT.Data;
using IRT.ModelParameters;

namespace IRT.ThetaEstimation
{
    public class BestThetaEstimator
    {
        // Valued pulled from top of page 379 in Ayala.
        private const double ConstantStep = .3;

        public double EstimateBestTheta(List<QuestionInfo> questionHistory, double previousTheta)
        {
            List<IModelParameters> modelParametersList = questionHistory.Select(x => x.Question.ModelParameters).ToList();
            List<int> responseVector = questionHistory.Select(x => (int)x.Score).ToList();

            if (IsZeroVarianceReponsePattern(responseVector))
            {
                return previousTheta + ConstantStep;
            }

            MaximumLikelihoodEstimator mleEstimator = new MaximumLikelihoodEstimator(modelParametersList);
            var mleOfTheta = mleEstimator.GetMle(responseVector);

            return mleOfTheta;
        }

        // MLE does not work well when the item reponses is either all correct or all incorrect.  See the bottom section on page 354 of Ayala for a discussion of this.
        // In this case, we use the strategy described in the paragraph spanning pages 378-379 of Ayala.
        private bool IsZeroVarianceReponsePattern(List<int> responseVector)
        {
            bool allResponsesIncorrect = responseVector.TrueForAll(x => x == 0);
            bool allResponsesCorrect = responseVector.TrueForAll(x => x == 1);

            return allResponsesCorrect || allResponsesIncorrect;
        }
    }
}
