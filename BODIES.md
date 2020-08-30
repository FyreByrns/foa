# Bodies

[BodyPartOwner.cs](FOA/Components/BodyPartOwner.cs)</br>
[BodyPart.cs](FOA/Components/BodyPart.cs)

Any Entity with a body must have the BodyPartOwner component.</br>
BodyPart components can only be added to entities which have a BodyPartOwner component.</br>

## BodyPart
BodyParts contain the name of the part they represent and a reference to the Entity which is the actual part.</br>
