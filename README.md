# QVT code generator

*Initial author: Erwan Bousse*

Generates C# code out of QVT model transformations that follow the [official QVT specification](http://www.omg.org/spec/QVT/).

## Requirements

- [NMF](https://github.com/NMFCode/NMF)

### Solutions

- `Common`: contains a VS solution with the projects commonly used by other solutions, such as utility classes for interacting with EnAr.
- `QvtEnginePerformance`: contains a VS solution with the QVT code generator. *Note: this solution references projects contained in the `Common` folder.*
- `QvtMetamodelCodeGeneration`: contains a standalone VS solution with the code used to generate the QVT metamodel code that can be found in the project `LL.MDE.Components.Qvt.Metamodel` of the solution/folder `QvtEnginePerformance`. It relies in the [NMF](https://github.com/NMFCode/NMF) framework to generate C# code from the official QVT Ecore model provided with the [official QVT specification](http://www.omg.org/spec/QVT/).
- `XSDImport2`: contains a VS solution with part of a WIP toolchain to import XSD files, ie. XSD file --[EMF]--> Ecore file --[NMF]--> In-memory NMF model --> EnAr metamodel. *Note: this solution references projects contained in the `Common` folder.*