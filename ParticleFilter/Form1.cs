using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleFilter
{
    public partial class Form1 : Form
    {

        int particleWidth = 1;
        int particleHeight = 15;

        int minRobotPosition = 15;
        int maxRobotPosition = 780;

        int door1Position = 156;
        int door2Position = 293;
        int door3Position = 548;
        int doorWidth = 46;

        double weightSum = 0f;

        bool isFirstIteration = true;

        int n;
        object sync = new object();

        Random rnd = new Random(DateTime.Now.Second);
        List<Particle> particles = new List<Particle>();
        List<DoorPosition> doorPositions = new List<DoorPosition>();
        List<Int32> imaginaryRobotPositions = new List<Int32>();
        Stopwatch stopWatch = new Stopwatch();

        int iteration = 0;
        int robotLocation = 361;

        public Form1()
        {
            InitializeComponent();
            doorPositions.Add(new DoorPosition(door1Position, door1Position + doorWidth));
            doorPositions.Add(new DoorPosition(door2Position, door2Position + doorWidth));
            doorPositions.Add(new DoorPosition(door3Position, door3Position + doorWidth));
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            status.Text = "Press Start to initial particles";
            //SetRobotOnRandomPosition();
            robot.Location = new Point(robotLocation, 203);
        }

        private void GenerateButton_Click(object sender, System.EventArgs e)
        {
            n = (int)numberOfParticles.Value;

            if (generatorBackgroundWorker.IsBusy != true)
            {
                generatorBackgroundWorker.RunWorkerAsync();
            }
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            ClearParticles();
            iteration = 0;
            particles.Clear();
            isFirstIteration = true;
            robot.Location = new Point(robotLocation, 203);
            if (generatorBackgroundWorker.IsBusy != true)
            {
                generatorBackgroundWorker.RunWorkerAsync();
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker worker = sender as BackgroundWorker;

            while (iteration < 200)
            {
                iteration++;

                if (isFirstIteration)
                {
                    GenerateParticles();
                    //ParallelGenerateParticles();
                    SensorUpdate();
                    Resample();
                    //ParallelResample();
                }
                else
                {
                    MotionUpdate();
                    SensorUpdate();
                    Resample();
                    //ParallelResample();
                }
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            particle_number.Text = "Time elapsed: " + stopWatch.ElapsedMilliseconds.ToString();
            status.Text = "Finished generating";
        }

        private void ParallelGenerateParticles()
        {
            stopWatch.Start();
            Parallel.ForEach(Enumerable.Range(0, n).ToList(), i =>
            {
                Particle particle = new Particle(rnd.Next(minRobotPosition, maxRobotPosition + 1), 1f / n);
                lock (sync)
                {
                    particles.Add(particle);
                }
                //DrawParticle(particle.position);
            });
            stopWatch.Stop();
            isFirstIteration = false;
        }

        private void GenerateParticles()
        {
            stopWatch.Start();
            for (int i = 0; i < n; i++)
            {
                Particle particle = new Particle(rnd.Next(minRobotPosition, maxRobotPosition + 1), 1f / n);

                particles.Add(particle);

                //DrawParticle(particle.position);
            }
            stopWatch.Stop();
            isFirstIteration = false;
        }

        private void SetRobotOnRandomPosition()
        {
            robot.Location = new Point(rnd.Next(minRobotPosition, maxRobotPosition + 1), 203);
        }

        private void DrawParticle(int position)
        {
            SolidBrush myBrush = new SolidBrush(Color.Red);
            Graphics formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(position, 275, particleWidth, particleHeight));
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private int FrontOfRobot()
        {
            return robot.Bounds.Location.X - ((particleWidth + 2 - 1) / 2) + (robot.Width / 2);
        }

        private void MotionUpdate()
        {
            int step = 15;
            Random rnd1 = new Random();
            int direction = rnd1.Next(0, 2);

            ClearParticles();

            if (robot.InvokeRequired)
            {
                robot.Invoke(new Action(MotionUpdate));
                return;
            }
            if (direction == 0)
            {
                if (robot.Bounds.Location.X - step > minRobotPosition)
                {
                    robot.Location = new Point(robot.Bounds.Location.X - step, 203);
                }
                else
                {
                    robot.Location = new Point(robot.Bounds.Location.X + step, 203);
                }
            }
            else
            {
                if (robot.Bounds.Location.X + step < maxRobotPosition)
                {
                    robot.Location = new Point(robot.Bounds.Location.X + step, 203);
                }
                else
                {
                    robot.Location = new Point(robot.Bounds.Location.X - step, 203);
                }
            }

            ParticleMotionUpdate(direction, step);
            //ParallelParticleMotionUpdate(direction, step);
        }

        private void ParticleMotionUpdate(int direction, int step)
        {
            stopWatch.Start();
            foreach (Particle particle in particles)
            {
                var randomStep = rnd.Next(step - 5, step + 6);
                if (direction == 0)
                {
                    particle.position -= randomStep;
                }
                else
                {
                    particle.position += randomStep;
                }

                //DrawParticle(particle.position);
            }
            stopWatch.Stop();
        }

        private void ParallelParticleMotionUpdate(int direction, int step)
        {
            stopWatch.Start();
            Parallel.ForEach(particles, particle =>
            {
                var randomStep = rnd.Next(step - 5, step + 6);
                if (direction == 0)
                {
                    particle.position -= randomStep;
                }
                else
                {
                    particle.position += randomStep;
                }

                //DrawParticle(particle.position);
            });
            stopWatch.Stop();
        }

        private void ClearParticles()
        {
            Graphics formGraphics = this.CreateGraphics();
            formGraphics.Clear(Color.White);
            formGraphics.Dispose();
        }

        private void SensorUpdate()
        {
            weightSum = 0f;
            imaginaryRobotPositions.Clear();
            for (int i = 0; i < 3; i++)
            {
                imaginaryRobotPositions.Add(RelativeDoorPositionTo(FrontOfRobot()) + doorPositions[i].startPosition);
            }

            CalculateParticleWeight();
            //ParallelCalculateParticleWeight();

            NormalizeWeights();
            //ParallelNormalizeWeights();
        }

        private void CalculateParticleWeight()
        {
            stopWatch.Start();
            foreach (Particle particle in particles)
            {
                if (IsInFrontOfDoor(FrontOfRobot()))
                {
                    if (!IsInProximity(particle.position))
                    {
                        particle.weight = 0.000001;
                        weightSum += particle.weight;
                    }
                    else
                    {
                        particle.weight = -Math.Pow((RelativeRobotPositionTo(particle.position)) / 23f, 2) + 1f;
                        weightSum += particle.weight;
                    }
                }
                else
                {
                    if (!IsInFrontOfDoor(particle.position))
                    {
                        particle.weight = 1;
                        weightSum += particle.weight;
                    }
                    else
                    {
                        particle.weight = Math.Pow((RelativeDoorPositionTo(particle.position) - 23f) / 23f, 2);
                        weightSum += particle.weight;
                    }
                }
            }
            stopWatch.Stop();
        }

        private void ParallelCalculateParticleWeight()
        {
            stopWatch.Start();
            Parallel.ForEach(particles, particle =>
            {
                if (IsInFrontOfDoor(FrontOfRobot()))
                {
                    if (!IsInProximity(particle.position))
                    {
                        particle.weight = 0.000001;
                        weightSum += particle.weight;
                    }
                    else
                    {
                        particle.weight = -Math.Pow((RelativeRobotPositionTo(particle.position)) / 23f, 2) + 1f;
                        weightSum += particle.weight;
                    }
                }
                else
                {
                    if (!IsInFrontOfDoor(particle.position))
                    {
                        particle.weight = 1;
                        weightSum += particle.weight;
                    }
                    else
                    {
                        particle.weight = Math.Pow((RelativeDoorPositionTo(particle.position) - 23f) / 23f, 2);
                        weightSum += particle.weight;
                    }
                }
            });
            stopWatch.Stop();
        }

        private void NormalizeWeights()
        {
            stopWatch.Start();
            foreach (Particle particle in particles)
            {
                particle.weight /= weightSum;
            }
            stopWatch.Stop();
        }

        private void ParallelNormalizeWeights()
        {
            stopWatch.Start();
            Parallel.ForEach(particles, particle =>
            {
                particle.weight /= weightSum;
            });
            stopWatch.Stop();
        }

        private void Resample()
        {
            List<Particle> resampledParticles = new List<Particle>();

            double r = rnd.NextDouble() * (Math.Pow(n, -1) - 0) + 0;
            double c = particles[0].weight;
            int i = 0;

            for (int m = 0; m < n; m++)
            {
                double u = r + m * Math.Pow(n, -1);
                while (u > c)
                {
                    i++;
                    c += particles[i].weight;
                }
                resampledParticles.Add(particles[i]);
            }

            resampledParticles = resampledParticles.Distinct().ToList();

            for (int j = 0; j < n - resampledParticles.Count; j++)
            {
                var random = rnd.Next(0, resampledParticles.Count);
                resampledParticles.Add(new Particle(resampledParticles[random].position + 1, resampledParticles[random].weight * 0.95));
            }

            particles.Clear();
            particles = resampledParticles;
        }

        private void ParallelResample()
        {
            List<Particle> resampledParticles = new List<Particle>();

            double r = rnd.NextDouble() * (Math.Pow(n, -1) - 0) + 0;
            double c = particles[0].weight;
            int i = 0;
            double u;

            Parallel.ForEach(Enumerable.Range(0, n).ToList(), m =>
            {
                lock(sync)
                {
                    u = r + (m - 1) * Math.Pow(n, -1);
                }              

                while (u > c)
                {
                    lock(sync)
                    {
                        i++;
                        c += particles[i].weight;
                    }
                }

                lock (sync)
                {
                    resampledParticles.Add(particles[i]);
                }
            });
            
            resampledParticles = resampledParticles.Distinct().ToList();

            for (int j = 0; j < n - resampledParticles.Count; j++)
            {
                var random = rnd.Next(0, resampledParticles.Count);
                resampledParticles.Add(new Particle(resampledParticles[random].position + 1, resampledParticles[random].weight * 0.95));
            }

            particles.Clear();
            particles = resampledParticles;
        }

        private int RelativeDoorPositionTo(int position)
        {
            if (IsInRange(position, doorPositions[0].startPosition, doorPositions[0].endPosition))
            {
                return position - door1Position;
            }
            else if (IsInRange(position, doorPositions[1].startPosition, doorPositions[1].endPosition))
            {
                return position - door2Position;
            }
            else
            {
                return position - door3Position;
            }
        }

        private int RelativeRobotPositionTo(int position)
        {
            if (IsInRange(position, imaginaryRobotPositions[0] - 23, imaginaryRobotPositions[0] + 23))
            {
                return position - imaginaryRobotPositions[0];
            }
            else if (IsInRange(position, imaginaryRobotPositions[1] - 23, imaginaryRobotPositions[1] + 23))
            {
                return position - imaginaryRobotPositions[1];
            }
            else
            {
                return position - imaginaryRobotPositions[2];
            }
        }

        private bool IsInFrontOfDoor(int position)
        {
            if (IsInRange(position, doorPositions[0].startPosition, doorPositions[0].endPosition) ||
                IsInRange(position, doorPositions[1].startPosition, doorPositions[1].endPosition) ||
                IsInRange(position, doorPositions[2].startPosition, doorPositions[2].endPosition))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsInRange(int value, int min, int max) => (uint)(value - min) <= (uint)(max - min);

        private bool IsInProximity(int position)
        {
            if (position - imaginaryRobotPositions[0] >= -23 && position - imaginaryRobotPositions[0] <= 23 ||
                position - imaginaryRobotPositions[1] >= -23 && position - imaginaryRobotPositions[1] <= 23 ||
                position - imaginaryRobotPositions[2] >= -23 && position - imaginaryRobotPositions[2] <= 23)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}