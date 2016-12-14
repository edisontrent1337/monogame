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
    public class FirstPersonCamera
    {
        private Vector3 cameraPosition;

        // holds the camera rotation along the main axis as eucledian angles in radians
        private Vector3 cameraRotationAngles;

        private Matrix rotationMatrix = Matrix.Identity;

        private float cameraSpeed;
        private Vector3 lookAt;

        private Vector3 velocity;
        private Vector3 acceleration;

        private Vector3 mouseRotationBuffer = new Vector3(0,0,0);

        private Vector3 cameraReference =  Vector3.UnitZ;

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private GraphicsDevice graphicsDevice;

        private Vector3 moveDir = Vector3.Zero;

        private Vector3 offSet = new Vector3(5, 1.8f, 5);


        private Matrix tangoPositionTransform = new Matrix(
            //new Vector4(-1, 0, 0, 1),
    new Vector4(-1, 0, 0, 1),
    new Vector4(0, 0, 1, 0),
    new Vector4(0, 1, 0, 0),
    new Vector4(0, 0, 0, 1)
    );

        private Viewport viewport;

        private const float SENSITIVITY = 0.1f;

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

        private const float ACCELERATION_RATE = 4;
        private const float MAX_VELOCITY = 2.3f;
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
                return cameraRotationAngles;
            }
            set
            {
                cameraRotationAngles = value;
                UpdateLookAt();
            }
        }


        public Matrix Projection
        {
            get;
            set;
        }

        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(cameraPosition, lookAt, Vector3.Up);
            }
        }


        public FirstPersonCamera(Vector3 position, Vector3 rotation, float cameraSpeed, GraphicsDevice graphicsDevice)
        {
            this.cameraPosition = position;
            this.cameraRotationAngles = rotation;
            this.cameraSpeed = cameraSpeed;
            this.graphicsDevice = graphicsDevice;
            this.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.Pi/4f, graphicsDevice.Viewport.AspectRatio,
                0.05f,
                1000f
            );
            this.velocity = new Vector3(0f, 0f, 0f);
            this.acceleration = new Vector3(0f, 0f, 0f);
            this.previousMouseState = Mouse.GetState();
            this.viewport = graphicsDevice.Viewport;
        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------------------------------------------------------


        public Viewport GetViewport()
        {
            return viewport;
        }
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
            rotationMatrix = Matrix.CreateRotationX(cameraRotationAngles.X) * Matrix.CreateRotationY(cameraRotationAngles.Y);
            Vector3 transformedCameraReference = Vector3.Transform(cameraReference, rotationMatrix);
            lookAt = cameraPosition + transformedCameraReference;
        }


        // UPDATE METHOD
        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool buttonPressed = false;

            currentMouseState = Mouse.GetState();
            acceleration = Vector3.Zero;

            KeyboardState state = Keyboard.GetState();

            Vector3 transformedCameraReference = Vector3.Transform(cameraReference, rotationMatrix);
            Vector3 transformedCameraPosition = Vector3.Transform(cameraPosition, rotationMatrix);

            if (state.IsKeyDown(Keys.W))
            {
                buttonPressed = true;
                acceleration.Z = ACCELERATION_RATE;
            }


            if (state.IsKeyDown(Keys.A))
            {
                acceleration.X = ACCELERATION_RATE;
            }



            if (state.IsKeyDown(Keys.S))
            {
                buttonPressed = true;
                acceleration.Z = -ACCELERATION_RATE;
            }



            if (state.IsKeyDown(Keys.D))
            {
                acceleration.X = -ACCELERATION_RATE;
            }


            if (buttonPressed)
            {
                acceleration.Y = transformedCameraReference.Y;
                if (Math.Abs(MathHelper.ToDegrees(transformedCameraReference.Y)) > 30)
                    moveDir.Y = transformedCameraPosition.Y - transformedCameraReference.Y;
                buttonPressed = false;
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

            if (velocity.Y > MAX_VELOCITY)
                velocity.Y = MAX_VELOCITY;
            if (velocity.Y < -MAX_VELOCITY)
                velocity.Y = -MAX_VELOCITY;

            if (velocity.Length() > MAX_VELOCITY)
            {
                velocity.Normalize();
                velocity *= MAX_VELOCITY;
            }

            if (acceleration.X == 0)
                velocity.X *= DAMPING;
            if (acceleration.Z == 0)
                velocity.Z *= DAMPING;
            if (acceleration.Y == 0)
                velocity.Y *= DAMPING;

            Move(velocity + moveDir, delta);


            moveDir = Vector3.Zero;
            float deltaX;
            float deltaY;

            if(currentMouseState != previousMouseState)
            {

                deltaX = currentMouseState.X - (graphicsDevice.Viewport.Width / 2);
                deltaY = currentMouseState.Y - (graphicsDevice.Viewport.Height / 2);

                mouseRotationBuffer.X -= SENSITIVITY * deltaX * delta;
                mouseRotationBuffer.Y -= SENSITIVITY * deltaY * delta;

                if (mouseRotationBuffer.Y < MathHelper.ToRadians(-75.0f))
                    mouseRotationBuffer.Y = MathHelper.ToRadians(-75.0f);

                if (mouseRotationBuffer.Y > MathHelper.ToRadians(75.0f))
                    mouseRotationBuffer.Y = MathHelper.ToRadians(75.0f);
            }
            //Console.WriteLine("MOUSE ROTATION BUFER Y :" + mouseRotationBuffer.Y);

            Rotation = new Vector3(-MathHelper.Clamp(mouseRotationBuffer.Y, MathHelper.ToRadians(-75.0f), MathHelper.ToRadians(75.0f)),
                MathHelper.WrapAngle(mouseRotationBuffer.X), 0);

            deltaX = 0;
            deltaY = 0;

            Mouse.SetPosition(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);

            previousMouseState = currentMouseState;

 
        }


        public void Update(Vector3 pos, Quaternion q)
        {
            //Position = pos;

            cameraPosition = pos + offSet;
            rotationMatrix = Matrix.CreateFromQuaternion(q);
            Vector3 transformedCameraReference = Vector3.Transform(cameraReference, rotationMatrix);
            lookAt = cameraPosition + transformedCameraReference;

        }
        public Vector3 GetLookAt()
        {
            return lookAt;
        }
        private Vector3 PreviewMove(Vector3 amount, float delta)
        {
            Matrix rotate = Matrix.CreateRotationY(cameraRotationAngles.Y);

            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);

            movement *= delta;
            movement = Vector3.Transform(movement, rotate);

            return cameraPosition + movement;
        }


        public void Move(Vector3 scale, float delta)
        {
            MoveTo(PreviewMove(scale, delta), Rotation);
            moveDir = Vector3.Zero;
        }

        public Vector3 GetAcceleration()
        {
            return acceleration;
        }

        public void SetAcceleration(Vector3 acceleration)
        {
            this.acceleration = acceleration;
        }

        public Vector3 GetVelocity()
        {
            return velocity;
        }


    }
}
