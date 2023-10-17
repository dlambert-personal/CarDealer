using Microsoft.VisualBasic;
using System.Reflection;

namespace DealerService.Models
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // Other utility methods ...
    }
    /// <summary>
    /// see https://ardalis.com/enum-alternatives-in-c/
    /// </summary>
    public class Brand : Enumeration
    { 
        public BrandParent? Parent { get; private set; }

        // see https://cars.usnews.com/cars-trucks/advice/car-brands-available-in-america

        public static Brand Acura = new(1, nameof(Acura), BrandParent.Honda);
        public static Brand AlfaRomeo = new(2, nameof(AlfaRomeo), BrandParent.Stellantis);
        public static Brand AstonMartin = new(3, nameof(AstonMartin));
        public static Brand Audi = new(4, nameof(Audi), BrandParent.VolkswagenGroup);
        public static Brand BMW = new(5, nameof(BMW), BrandParent.BMWGroup);
        public static Brand BentleyMotors = new(5, nameof(BentleyMotors), BrandParent.VolkswagenGroup);
        public static Brand Bugatti = new(5, nameof(Bugatti));  // rimac + VW
        public static Brand Buick = new(6, nameof(Buick), BrandParent.GeneralMotors);
        public static Brand Cadillac = new(7, nameof(Cadillac), BrandParent.GeneralMotors);

        public Brand() : base(0, "") { }
        public Brand(int id, string name)
            : base(id, name)
        {
        }

        public Brand(int id, string name, BrandParent parent)
            : base(id, name)
        {
            Parent = parent;
        }
    }
    public class BrandParent : Enumeration
    {
        // see https://cars.usnews.com/cars-trucks/advice/car-brands-available-in-america

        public static BrandParent BMWGroup = new(1, nameof(BMWGroup));
        public static BrandParent GeneralMotors = new(2, nameof(GeneralMotors));
        public static BrandParent Honda = new(3, nameof(Honda));
        public static BrandParent Stellantis = new(4, nameof(Stellantis));
        public static BrandParent VolkswagenGroup = new(10, nameof(VolkswagenGroup));

        public BrandParent() : base(0, "") { }
        public BrandParent(int id, string name)
            : base(id, name)
        {
        }
    }
}
