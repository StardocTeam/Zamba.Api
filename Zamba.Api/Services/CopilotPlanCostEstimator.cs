namespace Zamba.Api.Services;

public static class CopilotPlanCostEstimator
{
    public const int MonthlyCreditsIncluded = 1500;
    public const double MonthlyPlanCostUsd = 20d;
    public const double CostPerAdditionalCreditUsd = 0.02d;

    public static double? CalculateCreditsUsed(long? inputTokens, long? outputTokens)
    {
        if (!inputTokens.HasValue && !outputTokens.HasValue)
        {
            return null;
        }

        return ((double)(inputTokens ?? 0) + (outputTokens ?? 0)) / 1000d;
    }

    public static double? CalculateCostAtOverageRateUsd(long? inputTokens, long? outputTokens)
    {
        var creditsUsed = CalculateCreditsUsed(inputTokens, outputTokens);
        return creditsUsed * CostPerAdditionalCreditUsd;
    }

    public static double CalculateMonthlyAdditionalCostUsd(long totalTokens)
    {
        var creditsExceeded = Math.Max(0d, totalTokens / 1000d - MonthlyCreditsIncluded);
        return creditsExceeded * CostPerAdditionalCreditUsd;
    }

    public static double CalculateMonthlyPlanCostUsd(long totalTokens) =>
        MonthlyPlanCostUsd + CalculateMonthlyAdditionalCostUsd(totalTokens);
}