using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Code
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create animals (pets) using the factory
            Animal dog = AnimalFactory.CreateAnimal("dog");
            Animal bird = AnimalFactory.CreateAnimal("bird");

            // Feeding behavior before changing strategy (default set by class)
            Console.WriteLine("Feeding Dog:");
            dog.Feed();

            Console.WriteLine("Feeding Bird:");
            bird.Feed();

            // Changing feeding strategy at runtime (dynamically)
            Console.WriteLine("Changing Bird's feeding strategy to Omnivore...");
            bird.SetFeedingStrategy(new OmnivoreFeeding());
            bird.Feed();

            Console.ReadLine();
        }
    }
    //Animal Factory - Creates concrete objects
    public class AnimalFactory
    {
        public static Animal CreateAnimal(string type)
        {
            switch (type.ToLower())
            {
                case "dog": return new Dog();
                case "cat": return new Cat();
                case "bird": return new Bird();
                default: throw new ArgumentException("Invalid animal type.");
            }
        }
    }

    // Feeding Strategy Factory
    public class FeedingStrategyFactory
    {
        public static IFeedingStrategy CreateFeedingStrategy(string type)
        {
            switch (type.ToLower())
            {
                case "carnivore": return new CarnivoreFeeding();
                case "herbivore": return new HerbivoreFeeding();
                case "omnivore": return new OmnivoreFeeding();
                default: throw new ArgumentException("Invalid feeding strategy type.");
            }
        }
    }
    // Feeding Strategy Interface
    public interface IFeedingStrategy
    {
        void Feed();
    }

    // Concrete Feeding Strategies
    public class CarnivoreFeeding : IFeedingStrategy
    {
        public void Feed()
        {
            Console.WriteLine("Feeding meat to this animal.");
        }
    }

    public class HerbivoreFeeding : IFeedingStrategy
    {
        public void Feed()
        {
            Console.WriteLine("Feeding plants to this animal.");
        }
    }

    public class OmnivoreFeeding : IFeedingStrategy
    {
        public void Feed()
        {
            Console.WriteLine("Feeding a mix of plants and meat to this animal.");
        }
    }
    // Abstract Animal Class
    public abstract class Animal
    {
        protected IFeedingStrategy feedingStrategy;

        public void SetFeedingStrategy(IFeedingStrategy strategy)
        {
            feedingStrategy = strategy;
        }

        public void Feed()
        {
            if (feedingStrategy != null)
            {
                feedingStrategy.Feed();
            }
            else
            {
                Console.WriteLine("Error: No feeding strategy set.");
            }
        }
    }

    // Concrete Animal Subclasses
    public class Dog : Animal
    {
        //Dogs eat meat
        public Dog()
        {
            feedingStrategy = new CarnivoreFeeding();
        }
    }

    public class Cat : Animal
    {
        //Cats eat meat ... sort of
        public Cat()
        {
            feedingStrategy = new CarnivoreFeeding();
        }
    }

    public class Bird : Animal
    {
        // By default birds eat plants (Geese), but (Hawks) are carnivores and (Chickens) are omnivores 
        public Bird()
        {
            feedingStrategy = new HerbivoreFeeding();
        }
    }

}
