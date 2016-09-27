# OneOf

> install-package OneOf


This library provides F# style discriminated unions for C#, using a custom type `OneOf<T0, ... Tn>`. An instance of this type holds a single value, which is one of the types in its generic argument list.

Use cases
---------

You can use this as a parameter type, allowing a caller to pass different types without requiring additional overloads. This might not seem that useful for a single parameter, but if you have multiple parameters, the numer of overloads required increases rapidly.

```C#

// This method can be called with either a string, a ColorName enum value or a Color instance.
public void SetBackground(OneOf<string, ColorName, Color> backgroundColor) { ... }

```
Or as a return type, giving the ability to return strongly typed results without having to implement a type with a common base type or interface:

```C#
public OneOf<User, InvalidName, NameTaken> CreateUser(string username)
{
    if (!IsValid(username)) return new InvalidName();
    
    var user = _repo.FindByUsername(username);
    if (user != null) return new NameTaken();
    
    var user = new User(username);
    _repo.Save(user);
    
    return user;
}

```

Matching
--------

You use the `Match` method along with `When` and `Otherwise` chained-methods to get a value out. E.g.

```C#
public void SetBackground(OneOf<string, ColorName, Color> backgroundColor)
{
   Color c = backgroundColor.Match()
       .When((string str) => CssHelper.GetColorFromString(str))
       .When((ColorName name) => new Color(name))
       .When((Color col) => col)
   );
   
   _window.BackgroundColor = c;
}
```

Switching
---------

You use the `Switch` method along with `When` and `Otherwise` chained-methods to execute specific actions based on the value's type. E.g.

```C#
OneOf<string, NotFound> fileContents = ReadFile(fileName)
    .Switch()
    .When((string contents) => /* success */)
    .When((NotFound) => /* handle file not found */);
```

ToOneOf
--------

The `ToOneOf` method enables conversion to other OneOfs. E.g.

```C#
OneOf<True,False> trueOrFalse = True;

// this will work
OneOf<True,False,Unknown> trueFalseOrUnknown = trueOrFalse.ToOneOf<True,False,Unknown>();

// this will fail at runtime as the source contains a value that isn't supported by the new OneOf
OneOf<False> justFalse = trueOrFalse.ToOneOf<False>();
```



