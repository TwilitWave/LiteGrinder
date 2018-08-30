using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PhysicsTesting
{
    public class Camera2d
    {
        // Borrowed some code from: http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
        private Matrix transform;
        private Vector2 pos; // Camera Position
        private float zoom = 1f;
        private Vector2 startPos;

        public Camera2d(Vector2 startPos)
        {
            pos = startPos;
            this.startPos = startPos;
        }

        // Get set position
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        // Sets and gets zoom
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } // Negative zoom will flip image
        }

        // Reset position
        public void Reset()
        {
            pos = startPos;
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            transform =  Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return transform;
        }
        public Matrix get_transformation2(GraphicsDevice graphicsDevice)
        {
            transform = Matrix.CreateTranslation(new Vector3(ConvertUnits.ToSimUnits(-pos.X), ConvertUnits.ToSimUnits(-pos.Y), 0)) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(ConvertUnits.ToSimUnits(graphicsDevice.Viewport.Width * 0.5f), ConvertUnits.ToSimUnits(graphicsDevice.Viewport.Height * 0.5f), 0));
            return transform;
        }
    }
}
