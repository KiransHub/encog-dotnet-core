
 namespace Encog.ML.Genetic.Crossover {
	
	using Encog.ML.Genetic.Genome;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	
	/// <summary>
	/// A simple cross over where genes are simply "spliced". Genes are allowed to
	/// repeat.
	/// </summary>
	///
	public class Splice : ICrossover {
	
		/// <summary>
		/// The cut length.
		/// </summary>
		///
		private readonly int cutLength;
	
		/// <summary>
		/// Create a slice crossover with the specified cut length.
		/// </summary>
		///
		/// <param name="theCutLength">The cut length.</param>
		public Splice(int theCutLength) {
			this.cutLength = theCutLength;
		}
	
		/// <summary>
		/// Assuming this chromosome is the "mother" mate with the passed in
		/// "father".
		/// </summary>
		///
		/// <param name="mother">The mother.</param>
		/// <param name="father">The father.</param>
		/// <param name="offspring1">Returns the first offspring</param>
		/// <param name="offspring2">Returns the second offspring.</param>
		public void Mate(Chromosome mother, Chromosome father,
				Chromosome offspring1, Chromosome offspring2) {
			int geneLength = mother.Genes.Count;
	
			// the chromosome must be cut at two positions, determine them
			int cutpoint1 = (int) ((new Random()).Next() * (geneLength - this.cutLength));
			int cutpoint2 = cutpoint1 + this.cutLength;
	
			// handle cut section
			for (int i = 0; i < geneLength; i++) {
				if (!((i < cutpoint1) || (i > cutpoint2))) {
					offspring1.GetGene(i).Copy(father.GetGene(i));
					offspring2.GetGene(i).Copy(mother.GetGene(i));
				}
			}
	
			// handle outer sections
			for (int i_0 = 0; i_0 < geneLength; i_0++) {
				if ((i_0 < cutpoint1) || (i_0 > cutpoint2)) {
					offspring1.GetGene(i_0).Copy(mother.GetGene(i_0));
					offspring2.GetGene(i_0).Copy(father.GetGene(i_0));
				}
			}
		}
	}
}