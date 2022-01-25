using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    class Particle
    {

        public int position = 0;
        public double weight = 0f;

        public Particle(int position, double weight)
        {
            this.position = position;
            this.weight = weight;
        }
    }
}
