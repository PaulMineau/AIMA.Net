namespace AIMA.Core.Logic.FOL.Inference
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AIMA.Core.Logic.FOL.Inference.Proof;

    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class InferenceResultPrinter
    {
        /**
         * Utility method for outputting InferenceResults in a formatted textual
         * representation.
         * 
         * @param ir
         *            an InferenceResult
         * @return a String representation of the InferenceResult.
         */
        public static String printInferenceResult(InferenceResult ir) {
		StringBuilder sb = new StringBuilder();

		sb.Append("InferenceResult.isTrue=" + ir.isTrue());
		sb.Append("\n");
		sb.Append("InferenceResult.isPossiblyFalse=" + ir.isPossiblyFalse());
		sb.Append("\n");
		sb.Append("InferenceResult.isUnknownDueToTimeout="
				+ ir.isUnknownDueToTimeout());
		sb.Append("\n");
		sb.Append("InferenceResult.isPartialResultDueToTimeout="
				+ ir.isPartialResultDueToTimeout());
		sb.Append("\n");
		sb.Append("InferenceResult.#Proofs=" + ir.getProofs().Count);
		sb.Append("\n");
		int proofNo = 0;
        List<Proof.Proof> proofs = ir.getProofs();
		foreach (Proof.Proof p in proofs) {
			proofNo++;
			sb.Append("InferenceResult.Proof#" + proofNo + "=\n"
					+ ProofPrinter.printProof(p));
		}

		return sb.ToString();
	}
    }
}