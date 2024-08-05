using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Infrastructure.Framework.Texts;
using GeneticSharp.Runner.UnityApp.Car;
using UnityEngine;

public class Tournament : SelectionBase
{
    protected int Size { get; set; }// size of the tournament

    public Tournament() : base(2)
    {
    }

    public Tournament(int size) : base(2)
    {
        Size = size;
    }



    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {

        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();// Current Population: We will select individuals from here 
        IList<IChromosome> parents = new List<IChromosome>(); // List that will return the individuals that will mate, i.e. that will undergo variation
    

        for (int i = 0; i < number; i++)
        {
            // Select "Size" random parents to do tournament
            int[] randomIndexes = RandomizationProvider.Current.GetUniqueInts(Size, 0, population.Count);
            CarChromosome winner = null;
            float winnerFitness = -1;

            // From a tournament of size "Size", select the best individual and add it to parents list.
            for (int k = 0; k < Size; k++)
            {
                /* Maximization */
                int individualIndex = randomIndexes[k];
                if ((winner == null) || population[individualIndex].Fitness > winnerFitness)
                {
                    winner = population[individualIndex];
                    winnerFitness = population[individualIndex].Fitness;
                }
            }
            parents.Add(winner);
        }

        return parents;
    }
}
