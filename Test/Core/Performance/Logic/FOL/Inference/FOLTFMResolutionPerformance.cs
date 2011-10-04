namespace aima.test.core.performance.logic.fol.inference;

using java.util.Set;

using org.junit.Test;

using AIMA.Core.Logic.FOL.Inference.FOLTFMResolution;
using AIMA.Core.Logic.FOL.Inference.InferenceResult;
using AIMA.Core.Logic.FOL.Inference.Trace.FOLTFMResolutionTracer;
using AIMA.Core.Logic.FOL.KB.Data.Clause;
using aima.test.core.unit.logic.fol.CommonFOLInferenceProcedureTests;

/**
 * @author Ciaran O'Reilly
 * 
 */
public class FOLTFMResolutionPerformance :
		CommonFOLInferenceProcedureTests {

	@Test
	public void testFullFOLKBLovesAnimalQueryKillsJackTunaFalse() {
		// This query will not return using TFM as keep expanding
		// clauses through resolution for this KB.
		FOLTFMResolution ip = new FOLTFMResolution(1000 * 1000);
		ip.setTracer(new RegressionFOLTFMResolutionTracer());
		testFullFOLKBLovesAnimalQueryKillsJackTunaFalse(ip, true);
	}

	private class RegressionFOLTFMResolutionTracer :
			FOLTFMResolutionTracer {
		private int outerCnt = 1;
		private int noPairsConsidered = 0;
		private int noPairsResolved = 0;
		private int maxClauseSizeSeen = 0;

		public void stepStartWhile(Set<Clause> clauses, int totalNoClauses,
				int totalNoNewCandidateClauses) {
			outerCnt = 1;

			System.Console.WriteLine("");
			System.Console.WriteLine("Total # clauses=" + totalNoClauses
					+ ", total # new candidate clauses="
					+ totalNoNewCandidateClauses);
		}

		public void stepOuterFor(Clause i) {
			System.Console.Write(" " + outerCnt);
			if (outerCnt % 50 == 0) {
				System.Console.WriteLine("");
			}
			outerCnt++;
		}

		public void stepInnerFor(Clause i, Clause j) {
			noPairsConsidered++;
		}

		public void stepResolved(Clause iFactor, Clause jFactor,
				Set<Clause> resolvents) {
			noPairsResolved++;

			Clause egLargestClause = null;
			for (Clause c : resolvents) {
				if (c.getNumberLiterals() > maxClauseSizeSeen) {
					egLargestClause = c;
					maxClauseSizeSeen = c.getNumberLiterals();
				}
			}
			if (null != egLargestClause) {
				System.Console.WriteLine("");
				System.Console.WriteLine("E.g. largest clause so far="
						+ maxClauseSizeSeen + ", " + egLargestClause);
				System.Console.WriteLine("i=" + iFactor);
				System.Console.WriteLine("j=" + jFactor);
			}
		}

		public void stepFinished(Set<Clause> clauses, InferenceResult result) {
			System.Console.WriteLine("Total # Pairs of Clauses Considered:"
					+ noPairsConsidered);
			System.Console.WriteLine("Total # Pairs of Clauses Resolved  :"
					+ noPairsResolved);
			noPairsConsidered = 0;
			noPairsResolved = 0;
			maxClauseSizeSeen = 0;
		}
	}
}