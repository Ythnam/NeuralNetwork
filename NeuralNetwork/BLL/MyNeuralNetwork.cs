using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.BLL
{
    class MyNeuralNetwork
    {
        private int[] layers; // nombre de neurone par couche
        private float[][] neurons; //nombre de neurones à chaque étape de profondeur
        private float[][][] weights; //nombre de poids entre chaque neurones de couche i et i+1
        private float fitness; //fitness du réseau pour l'algorithme génétique

        //public NeuralNetwork(int[] layersNumber)
        //{
        //    this.layers = layersNumber; // layerNumber = 3 (1 seule couche cachée)

        //}

        public MyNeuralNetwork()
        {
            this.layers = new int[3] { 2, 3, 1 }; // Test sur 2 entrée : x,y et en sortie x,y
            CreateNeurons();
            CreateWeight();
        }

        public MyNeuralNetwork(MyNeuralNetwork copyNetwork)
        {
            this.layers = new int[copyNetwork.layers.Length];
            for (int i = 0; i < copyNetwork.layers.Length; i++)
            {
                this.layers[i] = copyNetwork.layers[i];
            }

            CreateNeurons();
            CreateWeight();
            CopyWeights(copyNetwork.weights);
        }

        private void CopyWeights(float[][][] copyWeights)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = copyWeights[i][j][k];
                    }
                }
            }
        }


        public void CreateNeurons()
        {
            // créer une liste de 3 liste de neurones contenant chacune le nombre de neurones qu'on veut selon layer (1er couche 2, 2eme 3 et 3eme 2)
            List<float[]> neuronsList = new List<float[]>();
            foreach (int layer in layers)
            {
                neuronsList.Add(new float[layer]); // instancie une liste de longueur = nombre de neurone qu'on veut pour la couche i
                /* On vient donc d'ajouter 3 liste :
                 * La 1er de 2 nombres (2 entrées)
                 * Ensuite 3 neurones (hidden layer)
                 * Enfin 1 nombres (1 sorties)
                */
            }

            this.neurons = neuronsList.ToArray();
        }

        public void CreateWeight()
        {
            List<float[][]> weightsList = new List<float[][]>();
            Random random = new Random();

            for(int i = 1; i < this.layers.Length; i++)
            {
                List<float[]> layerWeightsList = new List<float[]>(); // créer une liste de poids par neurones
                int neuronsInPreviousLayer = this.layers[i - 1];

                foreach (float[] neuron in neurons)
                {
                    float[] neuronWeights = new float[neuronsInPreviousLayer]; // le nombre de poids d'un neurone dépends du nombre de neurones dans la couche précédente

                    for(int k = 0; k < neuronsInPreviousLayer; k++)
                    {
                        
                        neuronWeights[k] = (float) random.NextDouble();
                        if (random.NextDouble() >= 0.5)
                            neuronWeights[k] = - neuronWeights[k];
                    }

                layerWeightsList.Add(neuronWeights); // Ajout des poids aux neurones correspondants
                }

            weightsList.Add(layerWeightsList.ToArray());
            }

            weights = weightsList.ToArray();
        }

        public float[] FeedForward(float[] inputs)
        {
            //Ajout des inputs (ne se passe que sur les neurones de la couche 1)
            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }

            //On itere sur tout le réseau
            for (int i = 1; i < layers.Length; i++)
            {
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    float value = 0f;

                    for (int k = 0; k < neurons[i - 1].Length; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k]; //sum off all weights connections of this neuron weight their values in previous layer
                    }

                    neurons[i][j] = (float)Math.Tanh(value); //Hyperbolic tangent activation
                }
            }

            return neurons[neurons.Length - 1]; //return output layer
        }
    }
}
