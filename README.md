# QVT code generator

*Initial author: Erwan Bousse*

Generates C# code out of QVT model transformations that follow the [official QVT specification](http://www.omg.org/spec/QVT/).

## Requirements

- [NMF](https://github.com/NMFCode/NMF)

### Solutions

- `Common`: solution with  projects commonly used by other solutions.
- `QvtEnginePerformance`: solution with the QVT code generator. *Note: references projects contained in the `Common` folder.
- `QvtMetamodelCodeGeneration`: standalone solution to generate the QVT metamodel code that can be found in the project `LL.MDE.Components.Qvt.Metamodel` of the solution/folder `QvtEnginePerformance`. It relies in the [NMF](https://github.com/NMFCode/NMF) framework to generate C# code from the official QVT Ecore model provided with the [official QVT specification](http://www.omg.org/spec/QVT/).