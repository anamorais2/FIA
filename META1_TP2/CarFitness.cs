using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Chromosomes;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Linq;

namespace GeneticSharp.Runner.UnityApp.Car
{
    public class CarFitness : IFitness
    {
        public CarFitness()
        {
            ChromosomesToBeginEvaluation = new BlockingCollection<CarChromosome>();
            ChromosomesToEndEvaluation = new BlockingCollection<CarChromosome>();
        }

        public BlockingCollection<CarChromosome> ChromosomesToBeginEvaluation { get; private set; }
        public BlockingCollection<CarChromosome> ChromosomesToEndEvaluation { get; private set; }
        public double Evaluate(IChromosome chromosome)
        {
            var c = chromosome as CarChromosome;
            ChromosomesToBeginEvaluation.Add(c);

            float fitness = 0; 
            do
            {
                Thread.Sleep(1000);

                
                float Distance = c.Distance;
                float EllapsedTime = c.EllapsedTime;
                float NumberOfWheels = c.NumberOfWheels;
                float CarMass = c.CarMass;
                int RoadCompleted = c.RoadCompleted ? 1 : 0;

                List<float> Velocities = c.Velocities;
                float SumVelocities = c.SumVelocities;
                
                List<float> Accelerations = c.Accelerations;
                float SumAccelerations = c.SumAccelerations;

                List<float> Forces = c.Forces;
                float SumTotalForces = c.SumForces;

                /*YOUR CODE HERE*/
                /*Note que é executado ao longo da simulação*/

                //fitness = Distance;

                
                // Quanto mais longe o carro chega, melhor é o seu desempenho.
                float distanceWeight = 1.0f;
                fitness += Distance * distanceWeight;

                // Quanto mais tempo o carro leva para completar a pista, pior é o seu desempenho.
                float timeWeight = -0.5f;
                fitness += EllapsedTime * timeWeight;

                // Carros com mais rodas podem ser mais pesados e menos eficientes
                float wheelPenalty = -0.1f;
                fitness += NumberOfWheels * wheelPenalty;

                // Carros mais pesados podem ser mais difíceis de controlar
                float massPenalty = -0.05f;
                fitness += CarMass * massPenalty;

                // Carros que completam a pista têm um peso extra
                float completionBonus = 100.0f;
                fitness += RoadCompleted * completionBonus;

                float averageVelocityWeight = 0.2f;
                float averageVelocity = SumVelocities / Velocities.Count;
                fitness += averageVelocity * averageVelocityWeight;

                // Acelerações médias podem indicar um bom uso de potência.
                float averageAccelerationWeight = 0.1f;
                float averageAcceleration = SumAccelerations / Accelerations.Count;
                fitness += averageAcceleration * averageAccelerationWeight;

                // Forças médias podem indicar um bom uso de potência.
                float averageForceWeight = 0.1f;
                float averageForce = SumTotalForces / Forces.Count;
                fitness += averageForce * averageForceWeight;
                
                c.Fitness = fitness;

            } while (!c.Evaluated);

            ChromosomesToEndEvaluation.Add(c);


            do
            {
                Thread.Sleep(1000);
            } while (!c.Evaluated);

            /*O valor da variável fitness é o valor de aptidão do indivíduo*/

            return fitness;
        }

    }
}