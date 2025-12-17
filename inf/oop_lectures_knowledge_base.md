# База знаний по лекциям: ООП, SOLID, GRASP, паттерны проектирования и многослойные архитектуры (C#)

> Этот файл собран из конспектов/презентаций курса. Цель: дать **единый справочник** по теории и шаблонам кода, чтобы по нему можно было уверенно делать лабораторные/домашние работы.

## Содержание
- [1. Основы ООП](#1-основы-ооп)
- [2. SOLID](#2-solid)
- [3. GRASP](#3-grasp)
- [4. Порождающие паттерны](#4-порождающие-паттерны)
- [5. Структурные паттерны](#5-структурные-паттерны)
- [6. Поведенческие паттерны (1)](#6-поведенческие-паттерны-1)
- [7. Поведенческие паттерны (2)](#7-поведенческие-паттерны-2)
- [8. Многослойные архитектуры](#8-многослойные-архитектуры)
- [9. Быстрые чек-листы для работы](#9-быстрые-чек-листы-для-работы)

---

## 1. Основы ООП

### 1.1. Парадигмы (контекст)
- **Структурное программирование**: управление потоком (циклы/ветвления), работа «на уровне инструкций».
- **Процедурное программирование**: декомпозиция через функции/процедуры (вынос повторяющихся фрагментов в методы).
- **ООП**: моделирование через **типы** и **объекты**, объединяющие **данные + поведения**.

### 1.2. Базовые определения
- **Тип** — шаблон: какие данные и методы будет иметь объект.
- **Объект** — экземпляр типа во время выполнения, занимает память.

### 1.3. Ссылочные и значимые типы (C#)
- **Ссылочные типы (class)**:
  - данные объекта — в куче (heap),
  - на стеке (stack) хранится ссылка.
- **Значимые типы (struct)**:
  - данные хранятся «там, где находится объект структуры» (как значение).

### 1.4. Инкапсуляция
**Инкапсуляция** — объединение атрибутов и поведений (данных и методов) в рамках одного типа.

Зачем:
- улучшает структурированность,
- локализует логику относительно данных,
- упрощает изменения,
- снижает шанс «сломать инвариант» типа.

Пример идеи (упрощённо):
```csharp
public class BankAccount
{
    private decimal _balance;

    public BankAccount(decimal balance) => _balance = balance;

    public bool TryWithdraw(decimal amount)
    {
        if (amount > _balance) return false;
        _balance -= amount;
        return true;
    }
}
```

### 1.5. Инварианты данных и типа
- **Инвариант данных**: правила корректного состояния данных.
- **Инвариант типа**: данные + их инвариант + поведения (операции), которые умеют менять данные **так, чтобы инварианты сохранялись**.

Идея: если пользователь напрямую меняет поля — инварианты легко сломать, поэтому:
- поля прячем,
- меняем состояние через методы/свойства с проверками.

### 1.6. Наследование vs композиция (важно)
Ключевая мысль из лекций:  
**Для переиспользования логики чаще используем композицию, а не наследование.**

Антипаттерн (переиспользование данных через наследование):
```csharp
public class Deck { /* Cards + Shuffle */ }

public class Dealer : Deck
{
    public void StartGame()
    {
        Shuffle(); // наследуемся, чтобы просто дернуть Shuffle — плохая причина для наследования
    }
}
```

Лучше (композиция):
```csharp
public class Dealer
{
    private readonly Deck _deck;
    public Dealer(Deck deck) => _deck = deck;

    public void StartGame()
    {
        _deck.Shuffle();
    }
}
```

Почему композиция «лучше» в среднем:
- меньше связанность,
- проще тестировать,
- меньше риск «кривой иерархии» (LSP-ловушки).

### 1.7. Имутабельность и минимизация мутабельности
**Имутабельность** — данные не подразумевают изменение. В ООП часто достигается так:
- скрываем мутабельные структуры,
- наружу отдаём read-only представления.

Плохой вариант (слишком много «set;» и наружная мутабельная коллекция):
```csharp
public class StudentGroup
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<long> StudentIds { get; set; }  // снаружи можно делать что угодно
}
```

Лучше:
```csharp
public class StudentGroup
{
    private readonly HashSet<long> _studentIds = new();

    public StudentGroup(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public long Id { get; }
    public string Name { get; set; }

    public IReadOnlyCollection<long> StudentIds => _studentIds;

    public bool TryAddStudent(long studentId) => _studentIds.Add(studentId);
}
```

### 1.8. Обработка ошибок: исключения vs Result types
Идея: исключения — не единственный способ. Часто полезно возвращать **результат операции как тип**.

Пример из лекций (паттерн Result types):
```csharp
public abstract record AddStudentResult
{
    private AddStudentResult() { }
    public sealed record Success : AddStudentResult;
    public sealed record AlreadyMember : AddStudentResult;
    public sealed record StudentLimitReached(int Limit) : AddStudentResult;
}

public AddStudentResult AddStudent(long studentId)
{
    if (_studentsIds.Count == MaxStudentCount)
        return new AddStudentResult.StudentLimitReached(MaxStudentCount);

    if (_studentsIds.Add(studentId) is false)
        return new AddStudentResult.AlreadyMember();

    return new AddStudentResult.Success();
}
```

Плюсы:
- явный контракт API,
- проще тестировать ветки,
- меньше «скрытых» исключений.

### 1.9. Value Objects (объекты-значения)
Идея: многие инварианты удобнее держать не в «сервисах» и не в сущностях, а в **типе-значении**.

Проблема (decimal везде, инварианты разъезжаются):
```csharp
public class Account
{
    public decimal Balance { get; private set; }

    public void Withdraw(decimal value)
    {
        if (value < 0) throw new ArgumentException("Value cannot be negative");
        Balance -= value;
    }
}
```

Решение (VO `Money` контролирует инвариант «неотрицательно»):
```csharp
public readonly struct Money
{
    public Money(decimal value)
    {
        if (value < 0) throw new ArgumentException("Value cannot be negative");
        Value = value;
    }

    public decimal Value { get; }

    public static Money operator -(Money left, Money right) =>
        new Money(left.Value - right.Value);
}

public class Account
{
    public Money Balance { get; private set; }

    public void Withdraw(Money value) => Balance -= value;
}
```

### 1.10. Vertical Slices (идея декомпозиции)
На слайдах показана идея: делить систему можно не только слоями («инфраструктура/логика/представление»), но и «вертикальными срезами» (по фичам/юзкейсам), чтобы уменьшать связанность и лучше локализовать изменения.

---

## 2. SOLID

SOLID — набор принципов проектирования, цель: **low coupling / high cohesion**, расширяемость, тестируемость.

### 2.1. SRP — Single Responsibility Principle
**Тип должен иметь единственную причину для изменения.**

Антипример: генератор разных отчётов в одном типе.
```csharp
public class ReportGenerator
{
    public void GenerateExcelReport(OperationResult result) { /*...*/ }
    public void GeneratePdfReport(OperationResult result) { /*...*/ }
}
```

Пример соблюдения: общий интерфейс + разные реализации.
```csharp
public interface IReportGenerator
{
    void GenerateReport(OperationResult result);
}

public class ExcelReportGenerator : IReportGenerator
{
    public void GenerateReport(OperationResult result) { /*...*/ }
}

public class PdfReportGenerator : IReportGenerator
{
    public void GenerateReport(OperationResult result) { /*...*/ }
}
```

Слайды отдельно отмечают:
- **почему SRP часто нарушают**: проще (меньше абстракций), ниже порог входа, легче переиспользовать приватные методы;
- **последствия нарушения**: связанность бизнес-требований, сложнее тестировать, сложнее кастомизировать реализацию.

### 2.2. OCP — Open/Closed Principle
**Тип должен быть открыт для расширения, но закрыт для изменения.**

Антипример: `switch` по enum внутри метода.
```csharp
public enum BinaryOperation { Summation, Subtraction }

public class BinaryOperand
{
    private readonly int _left;
    private readonly int _right;

    public int Evaluate(BinaryOperation operation) =>
        operation switch
        {
            BinaryOperation.Summation => _left + _right,
            BinaryOperation.Subtraction => _left - _right,
            _ => throw new ArgumentOutOfRangeException()
        };
}
```

Соблюдение: полиморфизм + интерфейс операции.
```csharp
public interface IBinaryOperation
{
    int Evaluate(int left, int right);
}

public class Summation : IBinaryOperation
{
    public int Evaluate(int left, int right) => left + right;
}

public class Subtraction : IBinaryOperation
{
    public int Evaluate(int left, int right) => left - right;
}

public sealed class BinaryOperand
{
    private readonly int _left;
    private readonly int _right;

    public int Evaluate(IBinaryOperation operation) =>
        operation.Evaluate(_left, _right);
}
```

Проверка из лекций: «представьте, что вы пишете библиотеку» — если потребители могут расширять функциональность **не меняя исходники**, то OCP соблюдён.

### 2.3. LSP — Liskov Substitution Principle
**Наследники не должны ломать инварианты и интерфейс родительских типов.**

Антипример: `Penguin : Bird`, но `FlyTo` «умирает».
```csharp
public class Bird : Creature
{
    public virtual void FlyTo(Coordinate coordinate) { /*...*/ }
}

public class Penguin : Bird
{
    public override void FlyTo(Coordinate coordinate) => Die();
}
```

Проблема: код, который работает с `IEnumerable<Bird>`, ожидает, что `FlyTo` летает.

Решение из лекций: **правильные абстракции**.
```csharp
public interface ICreature { void Die(); }

public interface IFlyingCreature : ICreature
{
    void FlyTo(Coordinate coordinate);
}

public class Colibri : IFlyingCreature
{
    public void Die() { /*...*/ }
    public void FlyTo(Coordinate coordinate) { /*...*/ }
}

public class Bat : IFlyingCreature
{
    public void Die() { /*...*/ }
    public void FlyTo(Coordinate coordinate) { /*...*/ }
}
```

И используем `IEnumerable<IFlyingCreature>` там, где нужен полёт.

### 2.4. ISP — Interface Segregation Principle
**Маленькие интерфейсы под конкретную задачу лучше, чем «всё-умеющий».**

Антипример:
```csharp
public interface ICanAllDevice
{
    void Print();
    void PlayMusic();
    void BakeBread();
}
```

Почему плохо (слайды):
- абстракции обрастают лишним поведением,
- больше пространство для ошибок,
- ISP ≈ SRP для интерфейсов; нарушение ISP часто ведёт к нарушению SRP у реализаций.

Соблюдение:
```csharp
public interface IPrinter { void Print(); }
public interface IMusicPlayer { void Play(); }
public interface IBakery { void BakeBread(); }
```

### 2.5. DIP — Dependency Inversion Principle
**Реализации не должны зависеть от реализаций напрямую.**

Идея: зависеть от абстракций, а не от конкретных классов.  
Слайды отмечают последствия нарушения DIP:
- сильная связанность: замена реализации требует изменения зависимого кода,
- ограничения расширения,
- сложнее тестировать (невозможно по-настоящему изолировать).

Практический признак нарушения: внутри класса много `new ConcreteX()` вместо принятия зависимости через интерфейс/конструктор.

---

## 3. GRASP

GRASP (General Responsibility Assignment Software Principles) — принципы распределения ответственностей в объектной модели.

### 3.1. Information Expert
**Информация должна обрабатываться там, где она содержится.**

Антипример: сервис считает total cost, хотя данные внутри `Order`.
```csharp
public class ReceiptService
{
    public Receipt CalculateReceipt(Order customer)
    {
        var totalCost = customer.Items.Sum(x => x.Price * x.Quantity);
        return new Receipt(totalCost, DateTime.Now);
    }
}
```

Соблюдение: `OrderItem` знает свою стоимость, `Order` знает total cost.
```csharp
public record OrderItem(int Id, decimal Price, int Quantity)
{
    public decimal Cost => Price * Quantity;
}

public class Order
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items;

    public decimal TotalCost => _items.Sum(x => x.Cost);
}
```

Почему нарушение плохо (слайды):
- частый спутник SRP-нарушений,
- переиспользование: либо копипаста, либо нелогичная связь модулей,
- сложнее тестировать.

### 3.2. Creator
**Создание используемых объектов — ответственность типов, которые их используют.**

Антипример: сервис «снаружи» создаёт `OrderItem` и суёт в Order:
```csharp
public class OrderService
{
    public Order CreateDefaultOrder()
    {
        return new Order()
            .AddItem(new OrderItem(1, 100, 1))
            .AddItem(new OrderItem(2, 1000, 3));
    }
}
```

Соблюдение: `Order` получает *данные* и сам создаёт `OrderItem`.
```csharp
public class Order
{
    private readonly List<OrderItem> _items = new();

    public Order AddItem(int id, decimal price, int quantity)
    {
        _items.Add(new OrderItem(id, price, quantity));
        return this;
    }
}
```

Подводные камни (из лекций):
- появляется неявная связанность между конструктором модели и методом-создателем,
- создатель может начать собирать «слишком много» и нарушить SRP,
- пересборка объектов может бить по производительности.

### 3.3. Controller
**Отдельный объект контролирует выполнение юзкейса** (use-case controller).

Идея: не вешать обработку запросов/команд прямо на доменные сущности или UI-код, а выделять контроллер, который:
- принимает входные данные,
- дергает сервисы/юзкейсы,
- возвращает результат.

Скетч (по мотивам слайдов):
```csharp
public class AddUserController
{
    private readonly IUserService _userService;

    public AddUserController(IUserService userService) =>
        _userService = userService;

    public AddUserResult Handle(AddUserRequest request) =>
        _userService.AddUser(request);
}
```

### 3.4. Low Coupling / High Cohesion
- **Low Coupling**: меньше зависимостей между типами, проще менять/тестировать.
- **High Cohesion**: обязанности внутри типа «про одно», связаны между собой.

В лекциях также связывается с SRP: MVC, например, поддерживает SRP (high cohesion), но не гарантирует low coupling.

### 3.5. Polymorphism
Когда есть условная логика (часто `switch`/`if` по типу), её можно убрать через полиморфизм:
- интерфейс + разные реализации,
- диспетчеризация вызова на реализацию.

### 3.6. Pure Fabrication
Иногда полезно создать «искусственный» тип, которого нет в домене, чтобы:
- уменьшить связанность,
- повысить связность,
- не ломать SRP доменных типов.
Типичный пример: репозиторий, адаптер к внешнему API, логгер, маппер.

### 3.7. Indirection
Вставить промежуточную абстракцию/тип, чтобы развязать компоненты.
Часто применяется вместе с адаптером/фасадом/портами.

### 3.8. Protected Variations
Выделять места возможных изменений за интерфейсами/абстракциями, чтобы изменения локализовались (родственно OCP).

---

## 4. Порождающие паттерны

### 4.1. Фабричный метод (Factory Method)
**Вариативность создания объектов** через наследование/полиморфизм.

Идея: базовый класс содержит общую логику, а создание «продукта» делегируется в `CreateX()`.

Пример (платежи):
```csharp
public interface IPayment { decimal Amount { get; } }
public record CashPayment(decimal Amount) : IPayment;
public record BankPayment(decimal Amount, string ReceiverAccountId) : IPayment;

public abstract class PaymentCalculator
{
    public IPayment Calculate(Order order)
    {
        var totalCost = order.TotalCost;
        // apply discounts...
        return CreatePayment(totalCost);
    }

    protected abstract IPayment CreatePayment(decimal amount);
}

public sealed class CashPaymentCalculator : PaymentCalculator
{
    protected override IPayment CreatePayment(decimal amount) => new CashPayment(amount);
}

public sealed class BankPaymentCalculator : PaymentCalculator
{
    private readonly string _receiverAccountId;
    public BankPaymentCalculator(string receiverAccountId) => _receiverAccountId = receiverAccountId;

    protected override IPayment CreatePayment(decimal amount) =>
        new BankPayment(amount, _receiverAccountId);
}
```

Когда полезно:
- общая логика одинакова, меняется только способ создания продукта.

### 4.2. Абстрактная фабрика (Abstract Factory)
В лекциях показан переход к варианту, где **создание вынесено в отдельную абстракцию фабрики**, а калькуляторы получают фабрику через DI.

```csharp
public interface IPaymentFactory
{
    IPayment Create(decimal amount);
}

public class CashPaymentFactory : IPaymentFactory
{
    public IPayment Create(decimal amount) => new CashPayment(amount);
}

public class BankPaymentFactory : IPaymentFactory
{
    private readonly string _receiverAccountId;
    public BankPaymentFactory(string receiverAccountId) => _receiverAccountId = receiverAccountId;
    public IPayment Create(decimal amount) => new BankPayment(amount, _receiverAccountId);
}

public interface IPaymentCalculator
{
    IPayment Calculate(Order order);
}

public class PaymentCalculator : IPaymentCalculator
{
    private readonly IPaymentFactory _paymentFactory;
    public PaymentCalculator(IPaymentFactory paymentFactory) => _paymentFactory = paymentFactory;

    public IPayment Calculate(Order order)
    {
        var totalCost = order.TotalCost;
        // apply discounts...
        return _paymentFactory.Create(totalCost);
    }
}
```

Плюсы (слайды):
- «настоящее SRP» (калькулятор не зависит напрямую от конкретных реализаций платежей),
- соблюдение OCP: добавляем новые виды платежей и фабрики, не меняя калькуляторы.

### 4.3. Builder (Билдер)
**Отдельный тип собирает данные и создаёт объект.**

Простой вариант:
```csharp
public record Order(IEnumerable<OrderItem> Items);

public class OrderBuilder
{
    private readonly List<OrderItem> _items = new();

    public OrderBuilder WithItem(OrderItem item)
    {
        _items.Add(item);
        return this;
    }

    public Order Build() => new Order(_items.ToArray());
}
```

Использование:
```csharp
var builder = new OrderBuilder()
    .WithItem(new OrderItem(Price: 1337, Amount: 2));

AddDefaultItems(builder);
AddRequestedItems(builder);

Order order = builder.Build();
```

Также в лекциях выделяется идея **convenience builder**: когда конструктор перегружен параметрами, билдер делает создание удобным и при этом **модель не зависит от билдера**.

### 4.4. Prototype (Прототип)
Суть: создание новых объектов через `Clone()` (копирование существующего шаблона).

Обычно:
- когда создание дороговато,
- когда нужно «копировать с изменениями».

Упоминаются типовые реализации через интерфейсы наподобие:
```csharp
public interface IPrototype<T>
{
    T Clone();
}
```

(В презентации также поднимается тема «рекурсивных дженериков», чтобы `Clone` возвращал корректный тип.)

### 4.5. Singleton (Синглтон)
Один экземпляр на приложение.

Double-check locking:
```csharp
public class Singleton
{
    private static readonly object _lock = new();
    private static Singleton? _instance;

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (_instance is not null) return _instance;

            lock (_lock)
            {
                if (_instance is not null) return _instance;
                return _instance = new Singleton();
            }
        }
    }
}
```

Lazy-реализация:
```csharp
public class Singleton
{
    private static readonly Lazy<Singleton> _instance =
        new(() => new Singleton(), LazyThreadSafetyMode.ExecutionAndPublication);

    private Singleton() { }

    public static Singleton Instance => _instance.Value;
}
```

Режимы `LazyThreadSafetyMode` (из лекций): `None`, `PublicationOnly`, `ExecutionAndPublication`.

Недостатки (из лекций):
- тестирование (невозможно подменить в тестах),
- DI (нельзя нормально передать зависимости извне),
- жизненный цикл (статический стейт),
- доступ «из любого места» без контроля.

---

## 5. Структурные паттерны

### 5.1. Adapter (Адаптер)
**Промежуточный тип**, использующий объект одного типа, чтобы реализовать интерфейс другого типа.

Термины:
- `target` — целевой интерфейс,
- `adaptee` — то, что адаптируем,
- `adapter` — обертка: реализует `target`, содержит `adaptee`, перенаправляет вызовы.

Пример: разные хранилища логов приводим к `ILogStorage`.
```csharp
public interface ILogStorage { void Save(LogMessage message); }

public class PostgresLogStorage
{
    public void Save(string message, DateTime timeStamp, int severity) { /*...*/ }
}

public class PostgresLogStorageAdapter : ILogStorage
{
    private readonly PostgresLogStorage _storage;
    public PostgresLogStorageAdapter(PostgresLogStorage storage) => _storage = storage;

    public void Save(LogMessage message) =>
        _storage.Save(message.Message, message.DateTime, message.Severity.AsInteger());
}
```

Адаптивный рефакторинг (из лекций): можно сделать «в два шага» — сначала адаптер для новых использований, потом менять реализацию внутри.
```csharp
public interface IAsyncLogStorage { Task SaveAsync(LogMessage message); }

public class AsyncLogStorageAdapter : IAsyncLogStorage
{
    private readonly ILogStorage _storage;
    public AsyncLogStorageAdapter(ILogStorage storage) => _storage = storage;

    public Task SaveAsync(LogMessage message)
    {
        _storage.Save(message);
        return Task.CompletedTask;
    }
}
```

### 5.2. Bridge (Мост)
**Разделение объектной модели на абстракции разных уровней.**  
Верхний уровень использует нижний (как «мост»), и оба могут развиваться независимо.

Пример: `IControl` управляет `IDevice`.
```csharp
public interface IDevice
{
    bool IsEnabled { get; set; }
    int Channel { get; set; }
    int Volume { get; set; }
}

public interface IControl
{
    void ToggleEnabled();
    void ChannelForward();
    void ChannelBackward();
    void VolumeUp();
    void VolumeDown();
}

public class Control : IControl
{
    private readonly IDevice _device;
    public Control(IDevice device) => _device = device;

    public void ToggleEnabled() => _device.IsEnabled = !_device.IsEnabled;
    public void ChannelForward() => _device.Channel += 1;
    public void ChannelBackward() => _device.Channel -= 1;
    public void VolumeUp() => _device.Volume += 10;
    public void VolumeDown() => _device.Volume -= 10;
}
```

### 5.3. Composite (Компоновщик)
Позволяет работать **одинаково** с одиночными объектами и группами объектов через общий интерфейс.

Пример: графические компоненты.
```csharp
public interface IGraphicComponent
{
    void MoveBy(int x, int y);
    void Draw();
}

public class Circle : IGraphicComponent { /* MoveBy/Draw */ }
public class Square : IGraphicComponent { /* MoveBy/Draw */ }

public class GraphicComponentGroup : IGraphicComponent
{
    private readonly IReadOnlyCollection<IGraphicComponent> _components;
    public GraphicComponentGroup(IReadOnlyCollection<IGraphicComponent> components) =>
        _components = components;

    public void MoveBy(int x, int y)
    {
        foreach (var c in _components) c.MoveBy(x, y);
    }

    public void Draw()
    {
        foreach (var c in _components) c.Draw();
    }
}
```

### 5.4. Decorator (Декоратор)
Тип-обёртка над объектом абстракции: **расширяет** поведения объекта, добавляя новую логику.

Структура:
- абстракция (интерфейс),
- декоратор (реализует абстракцию + содержит объект абстракции),
- `decoratee` — оборачиваемый объект.

Пример: логирование:
```csharp
public interface IService { void DoStuff(DoStuffArgs args); }

public class Service : IService
{
    public void DoStuff(DoStuffArgs args) { /*...*/ }
}

public class LoggingServiceDecorator : IService
{
    private readonly IService _decoratee;
    private readonly ILogger _logger;

    public LoggingServiceDecorator(IService decoratee, ILogger logger)
    {
        _decoratee = decoratee;
        _logger = logger;
    }

    public void DoStuff(DoStuffArgs args)
    {
        _logger.Log(ArgsToLogMessage(args));
        _decoratee.DoStuff(args);
    }

    private static string ArgsToLogMessage(DoStuffArgs args) => /*...*/ "";
}
```

### 5.5. Proxy (Прокси)
Тип-обёртка, реализующая **контроль доступа** к объекту (который реализует ту же абстракцию).

Примеры видов прокси из лекций:
- **Virtual Proxy** (ленивая инициализация):
```csharp
public class VirtualServiceProxy : IService
{
    private readonly Lazy<IService> _service = new(() => new Service());

    public void DoOperation(OperationArgs args) => _service.Value.DoOperation(args);
}
```
- **Defensive Proxy** (валидация/авторизация):
```csharp
public class ServiceAuthorizationProxy : IService
{
    private readonly IService _service;
    private readonly IUserInfoProvider _userInfoProvider;

    public void DoOperation(OperationArgs args)
    {
        if (_userInfoProvider.GetUserInfo().IsAuthenticated)
            _service.DoOperation(args);
    }
}
```
- **Caching Proxy** (кеширование/мемоизация):
```csharp
public class CachingServiceProxy : IService
{
    private readonly IService _service;
    private readonly Dictionary<OperationArgs, OperationResult> _cache = new();

    public OperationResult DoOperation(OperationArgs args)
    {
        if (_cache.TryGetValue(args, out var result))
            return result;

        return _cache[args] = _service.DoOperation(args);
    }
}
```

**Decorator vs Proxy** (слайды):
- прокси контролирует (controlled dispatch), декоратор расширяет (extended dispatch),
- прокси может «имитировать наличие объекта» (виртуальный прокси), декоратору объект нужен,
- разные оттенки композиции (в лекциях отмечается: прокси часто ближе к ассоциации/агрегации, декоратор — к агрегации).

### 5.6. Facade (Фасад)
**Оркестрация** одной или набора сложных операций в одном типе.

Недостатки из лекций:
- риск «god-class»,
- потеря абстракций из-за переиспользования логики внутри фасада,
- тяжёлый рефакторинг/декомпозиция,
- полезно приводить взаимодействие к request-response модели.

### 5.7. Flyweight (Легковес)
Декомпозиция объектов: выделяем тяжёлые повторяющиеся данные в отдельные модели и переиспользуем их.

Пример (частицы и модель данных):
```csharp
public record ModelData(byte[] Value);
public record Particle(int X, int Y, ModelData Model);

public class ParticleFactory
{
    private readonly IAssetLoader _assetLoader;
    private readonly Dictionary<string, ModelData> _cache = new();

    public ParticleFactory(IAssetLoader assetLoader) => _assetLoader = assetLoader;

    public Particle Create(string modelName)
    {
        var model = _cache.TryGetValue(modelName, out var data)
            ? data
            : _cache[modelName] = new ModelData(_assetLoader.Load(modelName));

        return new Particle(0, 0, model);
    }
}
```

---

## 6. Поведенческие паттерны (1)

### 6.1. Observer (Наблюдатель)
Публикация событий + подписчики.

Слайды предлагают:
- интерфейс подписчика,
- абстракцию подписки (чтобы можно было отписаться, не раскрывая детали).

```csharp
public interface IBabyPoopSubscriber { void OnBabyPooped(); }
public interface IBabyPoopSubscription { void Unsubscribe(); }

public class Baby
{
    private readonly List<IBabyPoopSubscriber> _subscribers = new();

    public IBabyPoopSubscription Subscribe(IBabyPoopSubscriber subscriber)
    {
        _subscribers.Add(subscriber);
        return new Subscription(this, subscriber);
    }

    public void Poop()
    {
        Console.WriteLine("I have pooped");
        foreach (var s in _subscribers) s.OnBabyPooped();
    }

    private sealed class Subscription : IBabyPoopSubscription
    {
        private readonly Baby _baby;
        private readonly IBabyPoopSubscriber _subscriber;

        public Subscription(Baby baby, IBabyPoopSubscriber subscriber)
        {
            _baby = baby;
            _subscriber = subscriber;
        }

        public void Unsubscribe() => _baby._subscribers.Remove(_subscriber);
    }
}
```

### 6.2. Template Method (Шаблонный метод)
Параметризация логики через наследование: общая логика в базовом классе, «шаги» — в наследниках.

Структура:
- шаблонный класс (abstract),
- шаблонный метод (общая логика),
- абстрактные шаги,
- конкретные классы (реализации шагов).

Пример (оценка сотрудников):
```csharp
public abstract class EmployeeEvaluatorBase
{
    public IEnumerable<Employee> Evaluate(IEnumerable<RatedEmployee> employees)
    {
        var sortedEmployees = SortEmployees(employees);
        return sortedEmployees.Select(x => x.Employee);
    }

    protected abstract IOrderedEnumerable<RatedEmployee> SortEmployees(IEnumerable<RatedEmployee> employees);
}

public sealed class TaskEmployeeEvaluator : EmployeeEvaluatorBase
{
    protected override IOrderedEnumerable<RatedEmployee> SortEmployees(IEnumerable<RatedEmployee> employees) =>
        employees.OrderByDescending(x => x.Rating.TaskCompletedCount);
}
```

Недостатки (из лекций):
- сильная связанность наследников с базовым классом,
- нельзя переиспользовать реализацию шагов,
- комбинаторный взрыв при >1 шагах,
- техническое нарушение SRP и потенциальное нарушение OCP.

### 6.3. Strategy (Стратегия)
Параметризация логики через **композицию и полиморфизм** (в отличие от Template Method).

Идея:
- выделить интерфейсы для варьируемых шагов,
- соединять их через композицию.

Скетч:
```csharp
public interface IEmployeeEvaluationCriterion
{
    IOrderedEnumerable<RatedEmployee> SortEmployees(IEnumerable<RatedEmployee> employees);
}

public sealed class TasksCriterion : IEmployeeEvaluationCriterion
{
    public IOrderedEnumerable<RatedEmployee> SortEmployees(IEnumerable<RatedEmployee> employees) =>
        employees.OrderByDescending(x => x.Rating.TaskCompletedCount);
}
```

### 6.4. Visitor (Посетитель)
Когда нужно делать операции над узлами сложной структуры (деревья, AST, файловая система), и не хочется:
- либо раздувать интерфейс узлов новыми методами,
- либо делать `switch` по типам.

Решение из лекций:
- интерфейс visitor с перегрузками `Visit(NodeType)`,
- узлы умеют `Accept(visitor)`,
- это **double dispatch**: динамический вызов `Accept`, затем статический выбор перегрузки `Visit`.

```csharp
public interface IFileSystemComponentVisitor
{
    void Visit(FileFileSystemComponent component);
    void Visit(DirectoryFileSystemComponent component);
}

public interface IFileSystemComponent
{
    void Accept(IFileSystemComponentVisitor visitor);
}

public sealed class FormattingVisitor : IFileSystemComponentVisitor
{
    private readonly StringBuilder _builder = new();
    private int _padding;

    public string Value => _builder.ToString();

    public void Visit(FileFileSystemComponent component)
    {
        _builder.Append(' ', _padding);
        _builder.AppendLine(component.Name);
    }

    public void Visit(DirectoryFileSystemComponent component)
    {
        _builder.Append(' ', _padding);
        _builder.AppendLine(component.Name);
        _padding += 1;

        foreach (IFileSystemComponent child in component.Components)
            child.Accept(this);

        _padding -= 1;
    }
}
```

### 6.5. Chain of Responsibility (Цепочка обязанностей)
Разбиваем операцию на «звенья», каждое:
- делает свою часть (часто валидацию),
- либо прекращает цепь,
- либо передаёт дальше.

Из лекций: полезно для SRP, уменьшения дублей, OCP (легко добавлять звенья).

Скелет:
```csharp
public interface IDiscountService
{
    DiscountResult Apply(DiscountRequestBase request);
}

public interface IDiscountLink : IDiscountService
{
    IDiscountLink AddNext(IDiscountLink link);
}

public abstract class DiscountLinkBase : IDiscountLink
{
    private IDiscountLink? _next;

    public abstract DiscountResult Apply(DiscountRequestBase request);

    public IDiscountLink AddNext(IDiscountLink link)
    {
        if (_next is null) _next = link;
        else _next.AddNext(link);

        return this;
    }

    protected DiscountResult CallNext(DiscountRequestBase request) =>
        _next?.Apply(request) ?? throw new InvalidOperationException("Chain missing terminal link");
}
```

Пример сборки цепочки фабрикой:
```csharp
public sealed class WebDiscountServiceFactory : IDiscountServiceFactory
{
    public IDiscountService Create() =>
        new TotalCostValidationLink()
            .AddNext(new CustomerStatusValidationLink())
            .AddNext(new CouponExpirationValidationLink())
            .AddNext(new BonusCostValidationLink());
}
```

---

## 7. Поведенческие паттерны (2)

### 7.1. Command (Команда)
Представление операции в виде объекта.

Структура:
- интерфейс команды + реализации,
- отправитель (создаёт команды),
- получатель (выполняет).

Слайды показывают кейс: внутри узла pipeline слишком много разных веток обработки документов → выделяем команды.

```csharp
public interface INodeCommand
{
    NodeCommandExecutionResult Execute(JsonDocument document);
}
```

Команды можно переиспользовать друг в друге (например, команда для массива использует команду для объекта).

### 7.2. Iterator (Итератор)
Вынос логики обхода коллекций/сложных структур в отдельные типы.

Проблематика (из лекций):
- логика обхода либо «протекает» в структуру,
- либо зависимый код завязывается на детали структуры,
- дубли обхода, низкая гибкость.

Паттерн:
- итератор (интерфейс/реализации),
- «коллекция» (создаёт итератор).

(В реальном C# часто выражается через `IEnumerable<T>`/`IEnumerator<T>` и `yield return`.)

### 7.3. Memento / Snapshot (Снимок)
Инкапсулируем сохранение и восстановление состояния.

Проблема: если «история» хранит сырые данные, легко нарушить инварианты/случайно испортить состояние.
Решение (из лекций):
- сущность сама создаёт снимок (`CreateSnapshot()`),
- сущность сама восстанавливает (`RestoreSnapshot(snapshot)`),
- «хранитель истории» работает со снимками.

```csharp
public sealed record TextFieldSnapshot(string Value, int Version);

public class TextField
{
    public string Value { get; private set; } = string.Empty;
    public int Version { get; private set; }

    public void AddValue(string value)
    {
        Value += value.Trim();
        Version += 1;
    }

    public TextFieldSnapshot CreateSnapshot() => new(Value, Version);

    public void RestoreSnapshot(TextFieldSnapshot snapshot)
    {
        Value = snapshot.Value;
        Version = snapshot.Version;
    }
}

public class TextFieldHistory
{
    private readonly TextField _field = new();
    private readonly Stack<TextFieldSnapshot> _snapshots = new();

    public void AddValue(string value)
    {
        _snapshots.Push(_field.CreateSnapshot());
        _field.AddValue(value);
    }

    public void RollbackLastOperation()
    {
        if (_snapshots.TryPop(out var snapshot))
            _field.RestoreSnapshot(snapshot);
    }
}
```

### 7.4. State (Состояние)
Выносим условную логику в зависимости от состояния сущности в отдельные типы.

Структура:
- интерфейс состояния + реализации,
- «ядро сущности» (данные и базовые операции с инвариантами),
- «сущность» как фасад, который делегирует в текущее состояние,
- состояния отвечают за допустимость переходов (машина состояний).

Пример (Submission):
```csharp
public class SubmissionCore
{
    public SubmissionCore(ISubmissionState state) => State = state;

    public DateTimeOffset? CompletedAt { get; private set; }
    public Points? Points { get; private set; }
    public ISubmissionState State { get; private set; }

    public void UpdateState(ISubmissionState state) => State = state;

    public void Rate(Points points, DateTimeOffset timestamp)
    {
        CompletedAt = timestamp;
        Points = points;
    }

    public void Ban() => Points = null;
}

public interface ISubmissionState
{
    bool TryActivate(SubmissionCore submission);
    bool TryDeactivate(SubmissionCore submission);
    bool TryRate(SubmissionCore submission, Points points, DateTimeOffset timestamp);
    bool TryBan(SubmissionCore submission);
}

public sealed class ActiveSubmissionState : ISubmissionState
{
    public bool TryActivate(SubmissionCore submission) => false;

    public bool TryDeactivate(SubmissionCore submission)
    {
        submission.UpdateState(new InactiveSubmissionState());
        return true;
    }

    public bool TryRate(SubmissionCore submission, Points points, DateTimeOffset timestamp)
    {
        submission.Rate(points, timestamp);
        submission.UpdateState(new CompletedSubmissionState());
        return true;
    }

    public bool TryBan(SubmissionCore submission)
    {
        submission.Ban();
        submission.UpdateState(new BannedSubmissionState());
        return true;
    }
}
```

Фасад сущности:
```csharp
public class Submission
{
    private readonly SubmissionCore _core;
    public Submission(SubmissionCore core) => _core = core;

    public DateTimeOffset? CompletedAt => _core.CompletedAt;
    public Points? Points => _core.Points;

    public bool TryActivate() => _core.State.TryActivate(_core);
    public bool TryDeactivate() => _core.State.TryDeactivate(_core);
    public bool TryRate(Points points, DateTimeOffset ts) => _core.State.TryRate(_core, points, ts);
    public bool TryBan() => _core.State.TryBan(_core);
}
```

Уточнение из лекций: это не единственно возможная реализация; можно хранить состояние и в фасаде, ядро может быть ответственным за переходы, можно не разделять ядро/фасад.

---

## 8. Многослойные архитектуры

### 8.1. Что такое «архитектура приложения»
Способ структурирования программных компонентов для управления сложностью.  
Критерий «приручения сложности» (из лекций): возможность расширять систему, добавляя реализации с минимальным влиянием на существующие решения. Основа: **low coupling + high cohesion**.

### 8.2. MVX (MVC)
- **Model**: содержит бизнес-логику; меняется при изменении юзкейсов/бизнес-правил.
- **View**: представление (GUI/CUI/API); меняется при изменении способа представления.
- **Controller**: посредник между model и view; меняется при изменении механизма взаимодействия.

Выводы из лекций:
- поддерживает SRP (high cohesion),
- не поддерживает low coupling (связи между частями часто всё равно сильные).

### 8.3. Трёхслойная архитектура (TLA)
Слои:
- **presentation**: представление + передача запросов в бизнес-логику,
- **business logic**: бизнес-правила/юзкейсы, использует data access,
- **data access**: персистентность данных.

Также рассматриваются два подхода к моделям:
- **анемичная модель**: типы без поведений, всё в сервисах,
- **богатая модель**: логика в типах домена.

Из лекций отдельно: «выпрямление зависимостей» (идея контролировать направление зависимостей, чтобы верхние слои не тащили инфраструктуру в домен).

### 8.4. Гексагональная архитектура (Ports & Adapters)
Причины появления (из лекций): развитие туллинга (веб-фреймворки, БД-фреймворки, библиотеки) → нужно **абстрагировать бизнес-логику от инфраструктуры**.

Структура:
- **input adapter** → **input port** → **business logic** → **output port** → **output adapter**
- и да: «не имеет ничего общего с шестиугольниками».

Выводы:
- абстракция приложения над инфраструктурой (output port),
- абстракция представления над приложением (input port),
- бизнес-логика независима от вспомогательных реализаций.

### 8.5. Что такое «бизнес логика» (как компоненты)
Из лекций:
- **бизнес правила**: данные + операции над ними, отражающие принципы домена (в ОО — типы домена),
- **юзкейсы**: операции, поддерживаемые приложением.

### 8.6. Луковая архитектура (Onion)
Основана на гексагональной; сильнее декомпозирует бизнес-логику.

Слои (как на слайдах):
- `presentation contracts`
- `application`
- `domain services`
- `domain model`
- `abstractions infrastructure`

Выводы:
- доменные модели описывают бизнес правила,
- доменные сервисы описывают юзкейсы,
- слой приложения — связующее звено между инфраструктурными абстракциями и доменом.

---

## 9. Быстрые чек-листы для работы

### 9.1. Чек-лист «нарушаю ли я SRP?»
- Мой класс меняется по 2+ причинам?
- В нём смешаны разные бизнес-требования?
- Его сложно тестировать изолированно?
- Он знает слишком много про разные части системы?

Если «да» → дроби тип, вводи абстракции, переносы ответственностей (GRASP: Information Expert, Pure Fabrication, Controller).

### 9.2. Чек-лист «мне нужен OCP?»
- Хочу добавлять новый вид поведения без правки старого кода?
- Есть `switch` по enum/типам, который постоянно растёт?
- Это библиотека/плагинная архитектура?

Если «да» → полиморфизм (Strategy), цепочка, фабрики, protected variations.

### 9.3. Когда какой паттерн брать (очень быстро)
- **Adapter**: есть несовместимые интерфейсы, но нужно единое API.
- **Bridge**: два измерения вариативности (например, control × device), нужно развивать независимо.
- **Composite**: дерево/иерархия «часть-целое», надо одинаково работать с элементом и группой.
- **Decorator**: добавить поведение без изменения класса и без контроля доступа.
- **Proxy**: контроль доступа/ленивая загрузка/кеширование, возможно «объекта нет».
- **Facade**: спрятать оркестрацию сложных подсистем (осторожно с god-class).
- **Flyweight**: много одинаковых тяжёлых данных → выносим и кешируем.

- **Observer**: события + подписки/отписки.
- **Template Method**: общая логика + вариативные шаги (через наследование).
- **Strategy**: вариативные шаги (через композицию) — чаще предпочтительнее Template Method.
- **Visitor**: много операций над структурой, но не хочется раздувать узлы или делать switch.
- **Chain of Responsibility**: пайплайн шагов/валидаций, возможность остановить цепь.
- **Command**: операция как объект (разделение инициирования и выполнения, переиспользование операций).
- **Iterator**: вынести обход структуры.
- **Memento**: откаты, история, снимки состояния.
- **State**: поведение зависит от состояния сущности, нужна явная машина состояний.

### 9.4. «Красные флаги» в коде
- Большой `switch`/`if-else` по типам/enum → думай про OCP/Polymorphism/Strategy/State/Visitor.
- Класс, который «делает всё» → SRP/Facade-risk.
- Наследование ради доступа к данным/методу → чаще композиция.
- Интерфейс на 20 методов → ISP.
- `new ConcreteX()` внутри бизнес-логики → DIP.

---

**Конец.**
