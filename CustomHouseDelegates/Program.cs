CustomHouse myCustomHouse = new CustomHouse("Таможня", new List<Transport> 
{ new Car("Skoda", "AE5555EA", 2020), new Car("Toyota", "AE7777EA", 2004),
new FreightCar("MAN", "AA2323AB", false), new FreightCar("Scania", "AA6868AB", true, "Products"),
new Bus("Isuzu", "KA2121BM"), new Bus("Iveco", "AA7979EE", 8)});

Message myMsg = CustomsChecking;

myCustomHouse.Checking(myMsg);



// Метод, выводящий информацию
void CustomsChecking(Transport transport)
{
    Console.WriteLine(transport.ToString()); 

    if(transport is Car)
        Console.WriteLine(((Car)transport).YearOfIssue > 2010 ? "\tВаш автомобиль прошел таможенный контроль." : 
            "\tПредьявите дополнительные документы для оценки технического состояния Вашего автомобиля.");
    else if(transport is FreightCar)
        Console.WriteLine(((FreightCar)transport).Cargo ? $"\tПройдите дополнительную проверку Вашего груза " +
            $"{((FreightCar)transport).TypeOfCargo}." : "\tВаш автомобиль прошел таможенный контроль.");
    else if(transport is Bus)
        Console.WriteLine(((Bus)transport).Passengers > 0 ? $"\tПройдите дополнительную проверку документов " +
            $"{((Bus)transport).Passengers} пассажиров" : "\tВаш автобус прошел таможенный контроль.");
}


// Делегат
delegate void Message(Transport transport);

// Классы 
class CustomHouse
{
    public CustomHouse(string name, List<Transport> transports)
    {
        Name = name;
        Transports = transports;
    }

    public string Name { get; set; }
    public List<Transport> Transports { get; set; }

    public void Checking(Message message)
    {
        Console.WriteLine($"Вас приветствует таможенный пункт {Name}.\n") ;
        foreach (var transport in Transports)   
            message.Invoke(transport);
    }
}

class Transport
{
    public Transport(string model, string number)
    {
        Model = model;
        Number = number;
    }

    public string Model { get; set; }
    public string Number { get; set; }

    // Поле объекта типа делегата
    public Message? CheckResult { get; set; }

    public override string ToString()
    {
        return $"ТC {Model}, гос. номер {Number}";
    }
}

class FreightCar : Transport
{
    public FreightCar(string model, string number, bool cargo, string typeOfCargo = null) : base(model, number)
    {
        Cargo = cargo;
        TypeOfCargo = typeOfCargo;  
    }

    public bool Cargo { get; set; } 
    public string? TypeOfCargo { get; set; }    
}


class Car : Transport
{
    public Car(string model, string number, int yearOfIssue) : base(model, number)
    {
        YearOfIssue = yearOfIssue;  
    }

    public int YearOfIssue { get; set; } 
}

class Bus : Transport
{
    public Bus(string model, string number, int passengers = 0) : base(model, number)
    {
        Passengers = passengers;
    }

    public int Passengers { get; set; } 
}