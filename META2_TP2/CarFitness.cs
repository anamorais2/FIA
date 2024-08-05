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

                //fitness = SumVelocities;

                //float distanceWeight = 1.0f;
                //fitness += Distance * distanceWeight;

                //// O tempo decorrido pode ser inversamente proporcional à aptidão - 
                //// menos tempo para completar é melhor.
                //float timeWeight = -0.5f;
                //fitness += EllapsedTime * timeWeight;

                //// Número de rodas pode afetar estabilidade e peso - 
                //// isso poderia ser ajustado dependendo do que a simulação valoriza.
                //float wheelPenalty = -0.1f;
                //fitness += NumberOfWheels * wheelPenalty;

                //// A massa do carro pode afetar a velocidade e a economia de combustível - 
                //// carros mais leves podem ser preferíveis.
                //float massPenalty = -0.05f;
                //fitness += CarMass * massPenalty;

                //// Completar a estrada é um objetivo claro, então dá-se um bônus por isso.
                //float completionBonus = 100.0f;
                //fitness += RoadCompleted * completionBonus;

                //// A velocidade média durante a simulação pode ser um indicador de desempenho.
                //float averageVelocityWeight = 0.2f;
                //float averageVelocity = SumVelocities / Velocities.Count;
                //fitness += averageVelocity * averageVelocityWeight;

                //// A aceleração média pode indicar a capacidade do carro de ganhar velocidade rapidamente.
                //float averageAccelerationWeight = 0.1f;
                //float averageAcceleration = SumAccelerations / Accelerations.Count;
                //fitness += averageAcceleration * averageAccelerationWeight;

                //// Forças médias podem indicar boa tração e uso eficiente de potência.
                //float averageForceWeight = 0.1f;
                //float averageForce = SumTotalForces / Forces.Count;
                //fitness += averageForce * averageForceWeight;

                //Main Function
                fitness = 0.3f * Distance + 100.0f * RoadCompleted + 2f * Distance / (EllapsedTime + 1.0f);

                //Extra Challenge Function
                //float maxVelocity = Velocities.Max();
                //fitness = 10000f * RoadCompleted + 40f * Distance + (5f * (1f / CarMass)) + (15f * (1f / NumberOfWheels)) + 200f * maxVelocity + 150f * (Distance / (EllapsedTime + 1.0f));
                //Console.WriteLine("Valor máximo das velocidades: " + maxVelocity);

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