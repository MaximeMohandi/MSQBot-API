using MSQBot_API.Core.Extension;
using MSQBot_API.Core.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSQBot_API.Core.Test.Helper
{
    internal record RateMock : IRate
    {
        internal RateMock(decimal? rate)
        {
            Rate = rate;
        }
        public decimal? Rate { get; init; }
    }

    public class RateParserTest
    {

        private static object[] listRateCase =
        {
            new object[] { new List<IRate?>
            {
                new RateMock(1.00M), new RateMock(5.02M),
                new RateMock(5.895M), new RateMock(10.00M)
            }},
            new object[] { new List<IRate?>
            {
                new RateMock(0M), new RateMock(0M),
                new RateMock(0M), new RateMock(0M)
            }},
            new object[] { new List<IRate?>
            {
                null, null,
                new RateMock(1.00M), new RateMock(50.00M)
            }},
        };


        [TestCaseSource(nameof(listRateCase))]
        public void GivenListRate_WhenHasRate_ThenTrue(List<IRate?> toCheck)
        {
            Assert.That(toCheck.HasRates(), Is.True);
        }

        [Test]
        public void GivenNull_WhenHasRate_ThenFalse()
        {
            List<IRate?> rateList = null;
            Assert.That(rateList.HasRates(), Is.False);
        }

        [Test]
        public void GivenListEmpty_WhenHasRate_ThenFalse()
        {
            Assert.That(RateOperationExtension.HasRates(new List<IRate?>()), Is.False);
        }

        [TestCase(1.50, ExpectedResult = 1.50)]
        [TestCase(8.788, ExpectedResult = 8.79)]
        [TestCase(5, ExpectedResult = 5.00)]
        [TestCase(0, ExpectedResult = 0.00)]
        [TestCase(null, ExpectedResult = null)]
        public decimal? GivenDecimalRate_WhenRoundRate_ThenDecimalRateRoundedTwoValue(decimal? toRound)
        {
            var rateMock = new RateMock(toRound);
            return rateMock.RoundRate();
        }

        [TestCaseSource(nameof(listRateCase))]
        public void GivenListDecimalRate_WhenAvgRate_ThenAverageRounded(List<IRate?> toAvg)
        {
            decimal avgExpected = 0;
            foreach (IRate? rate in toAvg)
            {
                if (rate is not null)
                {
                    avgExpected += rate.Rate is null ? 0 : rate.Rate.Value;
                }
            }
            avgExpected = Math.Round(avgExpected / toAvg.Count, 2);

            Assert.That(toAvg.AvgRate(), Is.EqualTo(avgExpected));
        }

        [Test]
        public void GivenListNull_WhenAvgRate_ThenNull()
        {
            List<IRate?> toAvg = null;
            Assert.That(toAvg.AvgRate(), Is.EqualTo(null));
        }

        [TestCaseSource(nameof(listRateCase))]
        public void GivenListDecimalRate_WhenMaxRate_ThenMaxRate(List<IRate?> toMax)
        {
            decimal? maxExpected = toMax.Max(r => r?.Rate);
            Assert.That(toMax.MaxRate(), Is.EqualTo(maxExpected));

        }

        [Test]
        public void GivenListNull_WhenMaxRate_ThenNull()
        {
            List<IRate?> toMax = null;
            Assert.That(toMax.MaxRate(), Is.EqualTo(null));
        }

        [TestCaseSource(nameof(listRateCase))]
        public void GivenListDecimalRate_WhenMinRate_ThenMaxRate(List<IRate?> toMax)
        {
            decimal? minExpected = toMax.Min(r => r?.Rate);
            Assert.That(toMax.MinRate(), Is.EqualTo(minExpected));

        }

        [Test]
        public void GivenListNull_WhenMinRate_ThenNull()
        {
            List<IRate?> toMin = null;
            Assert.That(toMin.MinRate(), Is.EqualTo(null));
        }
    }
}
