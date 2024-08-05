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

public class ParentSelection : SelectionBase
{
    public ParentSelection() : base(2)
    {
    }


    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {

        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();
        IList<IChromosome> parents = new List<IChromosome>();

        double sumFitness = 0.0f; // sum of the fitness of the population

        int k = 0;
        while (k < population.Count) // while the index is less than the size of the population
        {
            sumFitness += population[k].Fitness; // add the fitness of the chromosome to the sum
            k++;
        }

        for (int i = 0; i < number; i++) 
        { 
            double randomFitness = RandomizationProvider.Current.GetDouble(); // get a random number between 0 and the sum of the fitness
            float currentSum = 0.0f; // current sum of the fitness
            int j = 0; // index of the chromosome
            while (currentSum <= randomFitness) // while the current sum is less than the random number
            {
                currentSum += (float)(population[j].Fitness / sumFitness); // add the fitness of the chromosome to the current sum
                j++; // increment the index
            }
            parents.Add(population[j - 1]); // add the chromosome to the parents
                
        
        }

        return parents;
    }
}
