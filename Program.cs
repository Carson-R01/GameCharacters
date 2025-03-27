using NLog;
using System.Formats.Asn1;
using System.Reflection;
using System.Text.Json;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

// deserialize mario json from file into List<Mario>
string marioFileName = "mario.json";
string dkFile = "dk.json";
string sfFile = "sf2.json";
List<Mario> marios = [];
List<DonkeyKong> dkCharacters = [];
List<StreetFighter> sfCharacters = [];
// check if file exists
if (File.Exists(marioFileName))
{
  marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName))!;
  logger.Info($"File deserialized {marioFileName}");
}
if (File.Exists(dkFile))
{
  dkCharacters = JsonSerializer.Deserialize<List<DonkeyKong>>(File.ReadAllText(dkFile))!;
  logger.Info($"File deserialized {dkFile}");
}
if (File.Exists(sfFile))
{
  sfCharacters = JsonSerializer.Deserialize<List<StreetFighter>>(File.ReadAllText(sfFile))!;
  logger.Info($"File deserialized {sfFile}");
}


do
{
  // display choices to user
  Console.WriteLine("1) Display Mario Characters");
  Console.WriteLine("2) Add Mario Character");
  Console.WriteLine("3) Remove Mario Character");
  Console.WriteLine("4) Edit Mario Character");

  Console.WriteLine("5) Display Donkey Kong Characters");
  Console.WriteLine("6) Add Donkey Kong Character");
  Console.WriteLine("7) Remove Donkey Kong Character");
  Console.WriteLine("8) Edit Donkey Kong Character");

  Console.WriteLine("9) Display Street Fighter Characters");
  Console.WriteLine("10) Add Street Fighter Character");
  Console.WriteLine("11) Remove Street Fighter Character");
  Console.WriteLine("12) Remove Street Fighter Character");
  Console.WriteLine("Enter to quit");

  // input selection
  string? choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);

  if (choice == "1")
  {
    // Display Mario Characters
    foreach(var c in marios)
    {
      Console.WriteLine(c.Display());
    }
  }
  else if (choice == "2")
  {
    // Add Mario Character
    // Generate unique Id
    Mario mario = new()
    {
      Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
    };
    InputCharacter(mario);
    // Add Character
    marios.Add(mario);
    File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
    logger.Info($"Character added: {mario.Name}");
  }
  else if (choice == "3")
  {
    // Remove Mario Character
    Console.WriteLine("Enter the Id of the character to remove:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
      Mario? character = marios.FirstOrDefault(c => c.Id == Id);
      if (character == null)
      {
        logger.Error($"Character Id {Id} not found");
      } else {
        marios.Remove(character);
        // serialize list<marioCharacter> into json file
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"Character Id {Id} removed");
      }
    } else {
      logger.Error("Invalid Id");
    }
  } else if (string.IsNullOrEmpty(choice)) {
    break;
  } else {
    logger.Info("Invalid choice");
  }
  if (choice == "4")
{
    Console.WriteLine("Enter the ID of the Mario character to edit:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
        Mario? character = marios.FirstOrDefault(c => c.Id == Id);
        if (character == null)
        {
            logger.Error($"Character Id {Id} not found");
        }
        else
        {
            InputCharacter(character);
            File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
            logger.Info($"Character Id {Id} edited");
        }
    }
    else
    {
        logger.Error("Invalid Id");
    }
}
    if (choice == "5")
  {
    // Display Dk Characters
    foreach(var c in dkCharacters)
    {
      Console.WriteLine(c.Display());
    }
  }
  else if (choice == "6")
  {
    // Add Dk Character
    // Generate unique Id
    DonkeyKong dk = new()
    {
      Id = dkCharacters.Count == 0 ? 1 : dkCharacters.Max(c => c.Id) + 1
    };
    InputCharacter(dk);
    // Add Character
    dkCharacters.Add(dk);
    File.WriteAllText(dkFile, JsonSerializer.Serialize(dkCharacters));
    logger.Info($"Character added: {dk.Name}");
  }
    else if (choice == "7")
  {
    // Remove Dk Character
    Console.WriteLine("Enter the Id of the character to remove:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
      DonkeyKong? character = dkCharacters.FirstOrDefault(c => c.Id == Id);
      if (character == null)
      {
        logger.Error($"Character Id {Id} not found");
      } else {
        dkCharacters.Remove(character);
        // serialize list<dkCharacters> into json file
        File.WriteAllText(dkFile, JsonSerializer.Serialize(dkCharacters));
        logger.Info($"Character Id {Id} removed");
      }
    } else {
      logger.Error("Invalid Id");
    }
  } else if (string.IsNullOrEmpty(choice)) {
    break;
  } else {
    logger.Info("Invalid choice");
  }
  if (choice == "8") // Edit Donkey Kong Character
{
    Console.WriteLine("Enter the ID of the Donkey Kong character to edit:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
        DonkeyKong? character = dkCharacters.FirstOrDefault(c => c.Id == Id);
        if (character == null)
        {
            logger.Error($"Character Id {Id} not found");
        }
        else
        {
            InputCharacter(character);
            File.WriteAllText(dkFile, JsonSerializer.Serialize(dkCharacters));
            logger.Info($"Character Id {Id} edited");
        }
    }
    else
    {
        logger.Error("Invalid Id");
    }
}
      if (choice == "9")
  {
    // Display Sf2 Characters
    foreach(var c in sfCharacters)
    {
      Console.WriteLine(c.Display());
    }
  }
  else if (choice == "10")
  {
    // Add Sf2 Character
    // Generate unique Id
    StreetFighter sf = new()
    {
      Id = sfCharacters.Count == 0 ? 1 : sfCharacters.Max(c => c.Id) + 1
    };
    InputCharacter(sf);
    // Add Character
    sfCharacters.Add(sf);
    File.WriteAllText(sfFile, JsonSerializer.Serialize(sfCharacters));
    logger.Info($"Character added: {sf.Name}");
  }
    else if (choice == "11")
  {
    // Remove Sf2 Character
    Console.WriteLine("Enter the Id of the character to remove:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
      StreetFighter? character = sfCharacters.FirstOrDefault(c => c.Id == Id);
      if (character == null)
      {
        logger.Error($"Character Id {Id} not found");
      } else {
        sfCharacters.Remove(character);
        // serialize list<sfCharacters> into json file
        File.WriteAllText(sfFile, JsonSerializer.Serialize(sfCharacters));
        logger.Info($"Character Id {Id} removed");
      }
    } else {
      logger.Error("Invalid Id");
    }
  } else if (string.IsNullOrEmpty(choice)) {
    break;
  } else {
    logger.Info("Invalid choice");
  }
} while (true);

logger.Info("Program ended");

static void InputCharacter(Character character)
{
  Type type = character.GetType();
  PropertyInfo[] properties = type.GetProperties();
  var props = properties.Where(p => p.Name != "Id");
  foreach (PropertyInfo prop in props)
  {
    if (prop.PropertyType == typeof(string))
    {
      Console.WriteLine($"Enter {prop.Name}:");
      prop.SetValue(character, Console.ReadLine());
    } else if (prop.PropertyType == typeof(List<string>)) {
      List<string> list = [];
      do {
        Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");
        string response = Console.ReadLine()!;
        if (string.IsNullOrEmpty(response)){
          break;
        }
        list.Add(response);
      } while (true);
      prop.SetValue(character, list);
    }
  }
}