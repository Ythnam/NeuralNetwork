using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.BLL
{
    class NeuralNetwork
    {
        private int[] layers; // nombre de neurone par couche
        private float[][] neurons; //nombre de neurones à chaque étape de profondeur
        private float[][][] weights; //nombre de poids entre chaque neurones de couche i et i+1
        private float fitness; //fitness du réseau pour l'algorithme génétique

        //public NeuralNetwork(int[] layersNumber)
        //{
        //    this.layers = layersNumber; // layerNumber = 3 (1 seule couche cachée)

        //}

        public NeuralNetwork()
        {
            this.layers = new int[3] { 2, 3, 2 }; // Test sur 2 entrée : x,y et en sortie x,y

        }

        public void CreateNeurons()
        {
            // créer une liste de 3 liste de neurones contenant chacune le nombre de neurones qu'on veut selon layer (1er couche 2, 2eme 3 et 3eme 2)
            List<float[]> neuronsList = new List<float[]>();
            foreach (int layer in layers)
            {
                neuronsList.Add(new float[layer]); // instancie une liste de longueur = nombre de neurone qu'on veut pour la couche i
            }

            this.neurons = neuronsList.ToArray();
        }

        public void CreateWeight()
        {
            List<float[][]> neuronsList = new List<float[][]>();

        }
    }
}
