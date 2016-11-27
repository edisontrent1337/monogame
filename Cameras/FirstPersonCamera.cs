using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace RainBase.Cameras
{
    class FirstPersonCamera
    {
        private Vector3 cameraPosition;
        private Vector3 cameraRotation;
        private float cameraSpeed;
        private Vector3 lookAt;

        private Vector3 velocity;
        private Vector3 acceleration;

        private Vector3 mouseRotationBuffer;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private GraphicsDevice graphicsDevice;

        public Vector3 Velocity
        {
            get {
                return velocity;
            }
                
            set
            {
                velocity = value;
            }
        }

        public Vector3 Acceleration
        {
            get
            {
                return acceleration;
            }
            set
            {
                acceleration = value;
            }
        }

        private const float ACCELERATION_RATE = 32;
        private const float MAX_VELOCITY = 25;
        private const float DAMPING = 0.45f;


        public Vector3 Position
        {
            get
            {
                return cameraPosition;
            }

            set
            {
                cameraPosition = value;
                UpdateLookAt();
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return cameraRotation;
            }
            set
            {
                cameraRotation = value;
                UpdateLookAt();
            }
        }


        public Matrix Projection
        {
            get;
            protected set;
        }

        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(cameraPosition, lookAt, Vector3.Up);
            }
        }


        public FirstPersonCamera(Vector3 position, Vector3 rotation, float cameraSpeed, GraphicsDevice device)
        {
            this.cameraPosition = position;
            this.cameraRotation = rotation;
            this.cameraSpeed = cameraSpeed;
            this.graphicsDevice = device;
            this.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, graphicsDevice.Viewport.AspectRatio,
                0.05f,
                1000f
                );

            this.velocity = new Vector3(0f, 0f, 0f);
            this.acceleration = new Vector3(0f, 0f, 0f);

            this.previousMouseState = Mouse.GetState();
        }
        

        // METHODS & FUNCTIONS
        // -----------------------------------------------------------------------------------------------

        private void MoveTo(Vector3 position, Vector3 rotation)
        {
            // CHANGES THE PROPERTIES Position AND Rotation
            Position = position;
            Rotation = rotation;
        }


        // UPDATE LOOKAT VECTOR

        private void UpdateLookAt()
        {
            // ROTATIONSMATRIX
            Matrix rotationMatrix = Matrix.CreateRotationX(cameraRotation.X) * Matrix.CreateRotationY(cameraRotation.Y);

            Vector3 lookAtOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);

            lookAt = cameraPosition + lookAtOffset;

        }

        // UPDATE METHOD
        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentMouseState = Mouse.GetState();

            acceleration = Vector3.Zero;


            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W))
            {
                acceleration.Z = ACCELERATION_RATE;
            }


            if (state.IsKeyDown(Keys.A))
            {
                acceleration.X = ACCELERATION_RATE;
            }



            if (state.IsKeyDown(Keys.S))
            {
                acceleration.Z = -ACCELERATION_RATE;
            }


            if (state.IsKeyDown(Keys.D))
            {
                acceleration.X = -ACCELERATION_RATE;
            }


            if (acceleration != Vector3.Zero)
            {
                acceleration.Normalize();
                acceleration *= ACCELERATION_RATE;
                acceleration *= delta;
                velocity += acceleration;
            }

            if (velocity.X > MAX_VELOCITY)
                velocity.X = MAX_VELOCITY;
            if (velocity.X < -MAX_VELOCITY)
                velocity.X = -MAX_VELOCITY;

            if (velocity.Z > MAX_VELOCITY)
                velocity.Z = MAX_VELOCITY;
            if (velocity.Z < -MAX_VELOCITY)
                velocity.Z = -MAX_VELOCITY;

            if (velocity.Length() > MAX_VELOCITY)
            {
                velocity.Normalize();
                velocity *= MAX_VELOCITY;
            }

            if (acceleration.X == 0)
                velocity.X *= DAMPING;
            if (acceleration.Z == 0)
                velocity.Z *= DAMPING;

            Move(velocity, delta);


            float deltaX;
            float deltaY;

            if(currentMouseState != previousMouseState)
            {
                //Cache mouse location
                deltaX = currentMouseState.X - (graphicsDevice.Viewport.Width / 2);
                deltaY = currentMouseState.Y - (graphicsDevice.Viewport.Height / 2);

                mouseRotationBuffer.X -= 0.1f * deltaX * delta;
                mouseRotationBuffer.Y -= 0.1f * deltaY * delta;

                if (mouseRotationBuffer.Y < MathHelper.ToRadians(-75.0f))
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(-75.0f));

                if (mouseRotationBuffer.Y > MathHelper.ToRadians(75.0f))
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y + (mouseRotationBuffer.Y - MathHelper.ToRadians(75.0f));
            }

            Rotation = new Vector3(-MathHelper.Clamp(mouseRotationBuffer.Y, MathHelper.ToRadians(-75.0f), MathHelper.ToRadians(75.0f)),
                MathHelper.WrapAngle(mouseRotationBuffer.X), 0);

            deltaX = 0;
            deltaY = 0;

            Mouse.SetPosition(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);

            previousMouseState = currentMouseState;



        }


        private Vector3 PreviewMove(Vector3 amount, float delta)
        {
            Matrix rotate = Matrix.CreateRotationY(cameraRotation.Y);

            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement *= delta;
            movement = Vector3.Transform(movement, rotate);

            return cameraPosition + movement;
        }


        public void Move(Vector3 scale, float delta)
        {
            MoveTo(PreviewMove(scale, delta), Rotation);
        }

    }
}
