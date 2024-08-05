using System.Diagnostics;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Runner.UnityApp.Car;

public class MutationGaussian : IMutation
{
    public bool IsOrdered { get; private set; } // indicating whether the operator is ordered (if can keep the chromosome order).
    private double mutationIntensity = 1; // mutation intensity (0-1) - 1 is full mutation intensity (100%) and 0 is no mutation (0%) - default is 1 


    public MutationGaussian(double mutationIntensity) 
    {
        IsOrdered = true;
        this.mutationIntensity = mutationIntensity; // set the mutation intensity
    }

    public MutationGaussian()
    {
        IsOrdered = true;
    }

    public void Mutate(IChromosome chromosome, float probability)
    {
        CarChromosome newChromosome = new(((CarChromosome)chromosome).getConfig());

        for (int i = 0; i < newChromosome.Length; i++) // for each gene in the chromosome
        {
            if (RandomizationProvider.Current.GetDouble() <= probability) // if the probability is less than the random number
            {
               double geneValue = SampleGaussian((double)newChromosome.GetGene(i).Value, mutationIntensity); // get the gene value and add the mutation intensity
               newChromosome.ReplaceGene(i, new Gene(geneValue)); // replace the gene with the new gene value
               
            }

        }
        
    }

    protected double SampleGaussian(double mean, double stdDev) // sample a gaussian distribution
    {
        double u1 = RandomizationProvider.Current.GetDouble(0, 1); // get a random number between 0 and 1
        double u2 = RandomizationProvider.Current.GetDouble(0, 1); 
        double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) * System.Math.Sin(2.0 * System.Math.PI * u2); // calculate the random number
        return mean + stdDev * randStdNormal;// return the random number
    }

}
