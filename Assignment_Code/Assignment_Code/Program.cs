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
            Console.WriteLine("***FurEver Friends Animal Shelter***");
            Console.WriteLine("");

            Console.WriteLine("New pet rescued: Cat. Creating Object.");
            Animal cat = AnimalFactory.CreateAnimal("cat");
            // Feeding behavior before changing strategy (default set by class)
            Console.WriteLine("Feeding Cat:");
            cat.Feed();
            Console.WriteLine("");

            Console.WriteLine("New pet rescued: Bird. Creating Object.");
            Animal bird = AnimalFactory.CreateAnimal("bird");
            // Feeding behavior before changing strategy (default set by class)
            Console.WriteLine("Feeding Bird:");
            bird.Feed();

            Console.WriteLine("");
            // Changing feeding strategy at runtime (dynamically)
            Console.WriteLine("Changing Bird's feeding strategy to Omnivore...");
            Console.WriteLine("Feeding Bird:");
            // Both methods below  provide the same result
           // bird.SetFeedingStrategy(new OmnivoreFeeding()); 
            bird.SetFeedingStrategy(FeedingStrategyFactory.CreateFeedingStrategy("omnivore"));
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
                //Current pets in FurEver Friends
                case "dog": return new Dog();
                case "cat": return new Cat();
                case "bird": return new Bird();
                default: throw new ArgumentException("Invalid animal type.");
            }
        }
    }
    // Abstract Animal Class
    public abstract class Animal
    {
        protected IFeedingStrategy feedingStrategy; // Composition, an object of another class

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
        //Dogs eat meat and plants... sort of
        public Dog()
        {
            feedingStrategy = new OmnivoreFeeding();
        }
    }

    public class Cat : Animal
    {
        //Cats eat meat 
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

    // Creates a Feeding Strategy instance
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
    // Blueprint construct for declaring Feeding Strategies
    public interface IFeedingStrategy
    {
        // Based on what the animal (pet) eats, the console color changes
        //Carnivore = Red // Herbivore = Green // Omnivore = Dark Yellow
        void Feed();
    }

    // Concrete Feeding Strategies
    public class CarnivoreFeeding : IFeedingStrategy
    {
        public void Feed()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Feeding meat to this animal.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class HerbivoreFeeding : IFeedingStrategy
    {
        public void Feed()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Feeding plants to this animal.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class OmnivoreFeeding : IFeedingStrategy
    {
        public void Feed()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Feeding a mixture of plants and meat to this animal.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
   

}
