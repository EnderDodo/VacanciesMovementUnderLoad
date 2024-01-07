using System.Numerics;

namespace VacanciesMovementUnderLoad;

public class MyVector<T> where T :
    IEqualityOperators<T, T, bool>,
    IUnaryNegationOperators<T, T>,
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>
{
    public T X;
    public T Y;
    public T Z;

    public static MyVector<double> Zero => new MyVector<double>(0, 0, 0);
    public MyVector(T x, T y, T z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
    public MyVector(T[] coords)
    {
        X = coords[0];
        Y = coords[1];
        Z = coords[2];
    }

    public static double DistanceSquared(MyVector<double> left, MyVector<double> right)
    {
        return Math.Pow(left.X - right.X, 2) + Math.Pow(left.Y - right.Y, 2) + Math.Pow(left.Z - right.Z, 2);
    }

    public static double Length(MyVector<double> left)
    {
        return Math.Sqrt(MyVector<double>.DistanceSquared(left, MyVector<double>.Zero));
    }
    
    public static bool operator ==(MyVector<T>? left, MyVector<T>? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(MyVector<T>? left, MyVector<T>? right)
    {
        return !(left == right);
    }

    public static MyVector<T> operator -(MyVector<T> vector)
    {
        return new MyVector<T>(-vector.X, -vector.Y, -vector.Z);
    }

    public static MyVector<T> operator +(MyVector<T> left, MyVector<T> right)
    {
        return new MyVector<T>(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static MyVector<T> operator -(MyVector<T> left, MyVector<T> right)
    {
        return left + -right;
    }

    public static MyVector<T> operator *(MyVector<T> left, T right)
    {
        return new MyVector<T>(left.X * right, left.Y * right, left.Z * right);
    }
    public static MyVector<T> operator *(T right, MyVector<T> left)
    {
        return new MyVector<T>(left.X * right, left.Y * right, left.Z * right);
    }

    public static MyVector<T> operator /(MyVector<T> left, T right)
    {
        return new MyVector<T>(left.X / right, left.Y / right, left.Z / right);
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, null)) return false;
        if (GetType() != obj.GetType()) return false;
        if (ReferenceEquals(this, obj)) return true;

        var other = (MyVector<T>)obj;
        
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString()
    {
        return $"({X} {Y} {Z})";
    }
}