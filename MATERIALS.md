# Materials System

This system governs the properties of objects, allowing objects to interact with any object with a certain property.</br>
For example, fire should only burn flammable objects.</br>

Properties of an object are clear state changes, like "Aflame" or "Broken".</br>
Each property is bound to a [Systemic Variable](VARIABLES.md), and when that variable reaches a certain threshold, the property becomes active.</br>
When the variable no longer reaches the threshold, the property becomes inactive.</br>

[Material Profiles](FOA/MaterialProfile.cs) can be created to hold common properties which are shared between objects of the same material.</br>
For example, [wooden objects](FOA/MaterialProfile.cs#L18) would share "Aflame" and "Waterlogged", with similar thresholds.</br>
Current notation for declaring a property is `<Property Name>:<Condition 1> and <Condition 2> ...` where `<Condition>` is either `<Variable Name> <above/below> <Constant Name>` or `<Property Name>`
Current notation for declaring a profile is a list of strings, each in the format `<Property Name>:<Related Variable> <above/below> <threshold>`.

## Properties
[SystemicProperty.cs](FOA/SystemicProperty.cs)
 - Aflame
 - Frozen
 - Electrified
 - Waterlogged (may change to "Soaked" with variables for various different fluids)
 - Broken
 - Deteriorated

</br>*dev team, add more here*
