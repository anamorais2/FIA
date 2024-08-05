using GeneticSharp.Domain.Chromosomes;
using System;
using System.Linq;
using UnityEngine;
using GeneticSharp.Domain.Randomizations;
using System.Collections.Generic;
using GeneticSharp.Domain.Crossovers;

namespace GeneticSharp.Runner.UnityApp.Commons
{
    public class CrossoverUniform : ICrossover   // Recombinação uniforme
    {
 

        public int ParentsNumber { get; private set; }

        public int ChildrenNumber { get; private set; }

        public int MinChromosomeLength { get; private set; }

        public bool IsOrdered { get; private set; } // indicating whether the operator is ordered (if can keep the chromosome order).

        protected float crossoverProbability; // crossover probability (0-1) - 1 is full crossover probability (100%) and 0 is no crossover (0%) - default is 0.75


        public CrossoverUniform(float crossoverProbability) : this(2, 2, 2, true)
        {
            this.crossoverProbability = crossoverProbability;
        }

        public CrossoverUniform(int parentsNumber, int offSpringNumber, int minChromosomeLength, bool isOrdered)
        {
            ParentsNumber = parentsNumber;
            ChildrenNumber = offSpringNumber;
            MinChromosomeLength = minChromosomeLength;
            IsOrdered = isOrdered;
        }

        public IList<IChromosome> Cross(IList<IChromosome> parents)
        {
            IChromosome parent1 = parents[0];
            IChromosome parent2 = parents[1];
            IChromosome offspring1 = parent1.Clone();
            IChromosome offspring2 = parent2.Clone();

            if (RandomizationProvider.Current.GetDouble() <= crossoverProbability) // if the probability is less than the random number
            {
                int i = 0; 
                while (i < parent1.Length) // for each gene in the chromosome
                {
                    if (RandomizationProvider.Current.GetDouble() >= 0.5) // if the probability is greater than 0.5
                    {
                        offspring1.ReplaceGene(i, parent2.GetGene(i)); // replace the gene with the gene from the other parent
                        offspring2.ReplaceGene(i, parent1.GetGene(i)); // replace the gene with the gene from the other parent
                    }
                    i++;
                }
            }

            return new List<IChromosome> { offspring1, offspring2 }; // return the offspring
            
        }
    }
}