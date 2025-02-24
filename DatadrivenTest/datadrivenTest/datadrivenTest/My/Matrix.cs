﻿using System;
using System.Collections.Generic;
using System.Text;

namespace My
{
    struct Matrix3x3
    {
        public float M11;
        public float M12;
        public float M13;
        public float M21;
        public float M22;
        public float M23;
        public float M31;
        public float M32;
        public float M33;

        private static readonly Matrix3x3 IDENTITY = new Matrix3x3(1f, 0f, 0f,
                                                         0f, 1f, 0f,
                                                         0f, 0f, 1f);

        public Matrix3x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }
        public Matrix3x3(Vector3 row1, Vector3 row2, Vector3 row3)
        {
            M11 = row1.x;
            M12 = row1.y;
            M13 = row1.z;
            M21 = row2.x;
            M22 = row2.y;
            M23 = row2.z;
            M31 = row3.x;
            M32 = row3.y;
            M33 = row3.z;
        }

        public Vector2 Left
        {
            get { return new Vector2(-this.M11, -this.M12); }
            set { this.M11 = -value.x; this.M12 = -value.y; }
        }
        public Vector2 Right
        {
            get { return new Vector2(this.M11, this.M12); }
            set
            {
                this.M11 = value.x;
                this.M12 = value.y;
            }
        }
        public Vector2 Translation
        {
            get { return new Vector2(this.M31, this.M32); }
            set
            {
                M31 = value.x;
                M32 = value.y;
            }
        }
        public override string ToString()
        {
            return "[" + this.M11 + ", " + this.M12 + ", " + this.M13 + "]\r\n" +
                                "[" + this.M21 + ",\t" + this.M22 + ",\t" + this.M23 + "\r\n" + "]\r\n" +
                                "[" + this.M31 + ",\t" + this.M32 + ",\t" + this.M33 + "]\r\n";
        }

        public static Matrix3x3 Add(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 output;
            output.M11 = matrix1.M11 + matrix2.M11;
            output.M12 = matrix1.M12 + matrix2.M12;
            output.M13 = matrix1.M13 + matrix2.M13;
            output.M21 = matrix1.M21 + matrix2.M21;
            output.M22 = matrix1.M22 + matrix2.M22;
            output.M23 = matrix1.M23 + matrix2.M23;
            output.M31 = matrix1.M31 + matrix2.M31;
            output.M32 = matrix1.M32 + matrix2.M32;
            output.M33 = matrix1.M33 + matrix2.M33;
            return output;
        }

        public static Matrix3x3 Multiply(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 output;
            output.M11 = (matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21) + (matrix1.M13 * matrix2.M31);
            output.M12 = (matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22) + (matrix1.M13 * matrix2.M32);
            output.M13 = (matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23) + (matrix1.M13 * matrix2.M33);

            output.M21 = (matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21) + (matrix1.M23 * matrix2.M31);
            output.M22 = (matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22) + (matrix1.M23 * matrix2.M32);
            output.M23 = (matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23) + (matrix1.M23 * matrix2.M33);

            output.M31 = (matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21) + (matrix1.M33 * matrix2.M31);
            output.M32 = (matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22) + (matrix1.M33 * matrix2.M32);
            output.M33 = (matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23) + (matrix1.M33 * matrix2.M33);

            return output;
        }
        public static Vector3 Multiply(Vector3 vector, Matrix3x3 conversion)
        {
            Vector3 output;
            output.x = vector.x * conversion.M11 + vector.y * conversion.M21 + vector.z * conversion.M31;
            output.y = vector.x * conversion.M12 + vector.y * conversion.M22 + vector.z * conversion.M32;
            output.z = vector.x * conversion.M13 + vector.y * conversion.M23 + vector.z * conversion.M33;

            return output;
        }
        static Vector2 Multiply(Vector2 vector, Matrix3x3 conversion)
        {
            Vector2 output;
            output.x = vector.x * conversion.M11 + vector.y * conversion.M21 + conversion.M31;
            output.y = vector.x * conversion.M12 + vector.y * conversion.M22 + conversion.M32;

            return output;
        }

        public static Matrix3x3 operator *(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            return Multiply(matrix1, matrix2);
        }
        public static Vector2 operator *(Vector2 vector, Matrix3x3 conversion)
        {
            return Multiply(vector, conversion);
        }

        public static Matrix3x3 CreateRotation(float rag)
        {
            return new Matrix3x3(
                (float)Math.Cos(rag), (float)-Math.Sin(rag), 0,
                (float)Math.Sin(rag), (float)Math.Cos(rag), 0,
                0, 0, 1
                );
        }
        public static Matrix3x3 CreateTrancerate(Vector2 vec)
        {
            return new Matrix3x3(
                1, 0, 0,
                0, 1, 0,
                vec.x, vec.y, 1
                );
        }
        public static Matrix3x3 CreateTrancerate(Vector3 vec)
        {
            return new Matrix3x3(
                1, 0, 0,
                0, 1, 0,
                vec.x, vec.y, 1
                );
        }
    }

    struct Matrix4x4
    {
        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;

        public Matrix4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }
        public override string ToString()
        {
            return "[" + this.M11 + ", " + this.M12 + ", " + this.M13 + ", " + this.M14 + "]\r\n" +
                                "[" + this.M21 + ",\t" + this.M22 + ",\t" + this.M23 + "\r\n" + ", " + this.M24 + "]\r\n" +
                                "[" + this.M31 + ",\t" + this.M32 + ",\t" + this.M33 + ", " + this.M34 + "]\r\n" +
                                "[" + this.M41 + ",\t" + this.M42 + ",\t" + this.M43 + ", " + this.M44 + "]\r\n";
        }

        /// <summary>
        /// 平行移動値
        /// </summary>
        public Vector3 Translation
        {
            get { return new Vector3(this.M41, this.M42, this.M43); }
            set
            {
                M41 = value.x;
                M42 = value.y;
                M43 = value.z;
            }
        }
        /// <summary>
        /// 転置行列
        /// </summary>
        public Matrix4x4 Transpose
        {
            get
            {
                return new Matrix4x4(
                    M11, M21, M31, M41,
                    M12, M22, M32, M42,
                    M13, M23, M33, M43,
                    M14, M24, M34, M44
                    );
            }
        }

        /// <summary>
        /// 単位行列
        /// </summary>
        public static readonly Matrix4x4 Identity = new Matrix4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        /// <summary>
        /// 内積
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix4x4 operator *(Matrix4x4 matrix1, Matrix4x4 matrix2)
        {
            Matrix4x4 output;

            output.M11 = (matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21) + (matrix1.M13 * matrix2.M31) + (matrix1.M14 * matrix2.M41);
            output.M12 = (matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22) + (matrix1.M13 * matrix2.M32) + (matrix1.M14 * matrix2.M42);
            output.M13 = (matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23) + (matrix1.M13 * matrix2.M33) + (matrix1.M14 * matrix2.M43);
            output.M14 = (matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24) + (matrix1.M13 * matrix2.M34) + (matrix1.M14 * matrix2.M44);

            output.M21 = (matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21) + (matrix1.M23 * matrix2.M31) + (matrix1.M24 * matrix2.M41);
            output.M22 = (matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22) + (matrix1.M23 * matrix2.M32) + (matrix1.M24 * matrix2.M42);
            output.M23 = (matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23) + (matrix1.M23 * matrix2.M33) + (matrix1.M24 * matrix2.M43);
            output.M24 = (matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24) + (matrix1.M23 * matrix2.M34) + (matrix1.M24 * matrix2.M44);

            output.M31 = (matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21) + (matrix1.M33 * matrix2.M31) + (matrix1.M34 * matrix2.M41);
            output.M32 = (matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22) + (matrix1.M33 * matrix2.M32) + (matrix1.M34 * matrix2.M42);
            output.M33 = (matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23) + (matrix1.M33 * matrix2.M33) + (matrix1.M34 * matrix2.M43);
            output.M34 = (matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24) + (matrix1.M33 * matrix2.M34) + (matrix1.M34 * matrix2.M44);

            output.M41 = (matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21) + (matrix1.M43 * matrix2.M31) + (matrix1.M44 * matrix2.M41);
            output.M42 = (matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22) + (matrix1.M43 * matrix2.M32) + (matrix1.M44 * matrix2.M42);
            output.M43 = (matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23) + (matrix1.M43 * matrix2.M33) + (matrix1.M44 * matrix2.M43);
            output.M44 = (matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24) + (matrix1.M43 * matrix2.M34) + (matrix1.M44 * matrix2.M44);

            return output;
        }
        /// <summary>
        /// 内積
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 vector, Matrix4x4 conversion)
        {
            Vector3 output;

            output.x = vector.x * conversion.M11 + vector.y * conversion.M21 + vector.z * conversion.M31 + conversion.M41;
            output.y = vector.x * conversion.M12 + vector.y * conversion.M22 + vector.z * conversion.M32 + conversion.M41;
            output.z = vector.x * conversion.M13 + vector.y * conversion.M23 + vector.z * conversion.M33 + conversion.M41;

            return output;
        }
        /// <summary>
        /// X軸回転行列作成
        /// </summary>
        /// <param name="rag"></param>
        /// <returns></returns>
        public static Matrix4x4 CreateRotationX(float rag)
        {
            return new Matrix4x4(
                1, 0, 0, 0,
                0, MathF.Cos(rag), -MathF.Sin(rag), 0,
                0, MathF.Sin(rag), MathF.Cos(rag),  0,
                0, 0, 0, 1);
        }
        /// <summary>
        /// Y軸回転行列作成
        /// </summary>
        /// <param name="rag"></param>
        /// <returns></returns>
        public static Matrix4x4 CreateRotationY(float rag)
        {
            return new Matrix4x4(
                MathF.Cos(rag), 0, MathF.Sin(rag), 0,
                0, 1, 0, 0,
                -MathF.Sin(rag), 0, MathF.Cos(rag), 0,
                0, 0, 0, 1);
        }
        /// <summary>
        /// Z軸回転行列作成
        /// </summary>
        /// <param name="rag"></param>
        /// <returns></returns>
        public static Matrix4x4 CreateRotationZ(float rag)
        {
            return new Matrix4x4(
                MathF.Cos(rag), -MathF.Sin(rag), 0, 0,
                MathF.Sin(rag), MathF.Cos(rag), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }
        /// <summary>
        /// 平行移動行列作成
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Matrix4x4 CreateTrancerate(Vector3 vec)
        {
            return new Matrix4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                vec.x, vec.y, vec.z, 1);
        }
        /// <summary>
        /// 射影変換行列作成
        /// </summary>
        /// <param name="fieldOfView"></param>
        /// <param name="aspect"></param>
        /// <param name="nearPlaneDistance"></param>
        /// <param name="farPlaneDistance"></param>
        /// <returns></returns>
        public static Matrix4x4 CreatePerspectiveFieldOfView(float fieldOfView, float aspect, float nearPlaneDistance, float farPlaneDistance)
        {
            if (fieldOfView <= 0 || fieldOfView >= MathF.PI)
                throw new Exception("fieldOfView <= 0 or >= PI");
            if (nearPlaneDistance <= 0f)
                throw new ArgumentException("nearPlaneDistance <= 0");
            if (farPlaneDistance <= 0f)
                throw new ArgumentException("farPlaneDistance <= 0");
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentException("nearPlaneDistance >= farPlaneDistance");

            Matrix4x4 result;

            float num = 1f / ((float)MathF.Tan(fieldOfView * 0.5f));
            float num2 = num / aspect;
            float num3 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            float num4 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result = new Matrix4x4(
                num2, 0, 0, 0,
                0, num, 0, 0,
                0, 0, num3, -1,
                0, 0, num4, 0
                );

            return result;
        }
        public static Matrix4x4 ViewMatrix(Vector3 cameraPosition, Vector3 target, Vector3 cameraUpVector)
        {
            Vector3 a = (cameraPosition - target).Normalize;
            Vector3 b = (Vector3.Cross(cameraUpVector, a)).Normalize;
            Vector3 c = Vector3.Cross(a, b);

            return new Matrix4x4(
                b.x, c.x, a.x, 0f,
                b.y, c.y, a.y, 0f,
                b.z, c.z, a.z, 0f,
                -Vector3.Dot(b, cameraPosition), -Vector3.Dot(c, cameraPosition), -Vector3.Dot(a, cameraPosition), 1
                );
        }
    }
}
