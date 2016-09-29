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

`Match` is used for translating the value depending on it's type.  Each Match must translate the value to the same type.
When all cases are handled, the last call to `Match` returns the result.    
```C#
Color c = backgroundColor
   .Match((string str) => CssHelper.GetColorFromString(str))
   .Match((ColorName name) => new Color(name))
   .Match((Color col) => col)
```
```C#
`Otherwise` can be used when you don't want to match for every type, returning a default value for remaining types.
Color c2 = backgroundColor
   .Match((Color col) => col)
   .Otherwise(obj => /* return default value */)
```
`OtherwiseThrow` can be used to create an exception to throw when the value doesn't match any specified types.
```C#
Color c3 = backgroundColor
   .Match((Color col) => col)
   .OtherwiseThrow(obj => new InvalidOperationException("this will be thrown when not Color"))
```

Switching
---------

You use the `Switch` methods along with `Otherwise` and `OtherwiseThrow` chained-methods to execute specific actions based on the value's type. E.g.

```C#
OneOf<string, NotFound, ErrX, ErrY, Etc> fileContents = ReadFile(fileName)
    .Switch((string contents) => /* handled success */)
    .Switch((NotFound notFound) => /* handle file not found */)
    .Otherwise(object x => /* handle other types */)
    
OneOf<string, NotFound, ErrX, ErrY, Etc> fileContents = ReadFile(fileName)
    .Switch((string contents) => /* handled success */)
    .Switch((NotFound notFound) => /* handle file not found */)
    .OtherwiseThrow(x => /* return Exception to throw when not handled above by any Switch's */);
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
