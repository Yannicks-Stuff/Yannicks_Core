using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Yannick.Lang
{
    /// <summary>
    /// Represents a dynamic class builder that allows runtime creation of types, methods, properties, events, and operators.
    /// </summary>
    public sealed class DynamicClass
    {
        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly ModuleBuilder _moduleBuilder;
        private readonly TypeBuilder _typeBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicClass"/> class with a unique name.
        /// </summary>
        public DynamicClass() : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicClass"/> class with a specified name and optional parent class.
        /// </summary>
        /// <param name="name">The name of the dynamic class.</param>
        /// <param name="parent">The parent <see cref="DynamicClass"/> to inherit from.</param>
        public DynamicClass(string name, DynamicClass? parent = null)
        {
            AssemblyName = name;
            _assemblyBuilder =
                AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(AssemblyName),
                    AssemblyBuilderAccess.RunAndCollect);
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule("MainModule");
            _typeBuilder = _moduleBuilder.DefineType(
                AssemblyName,
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass |
                TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,
                parent?._typeBuilder);

            // Define default constructor
            _typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName |
                                                  MethodAttributes.RTSpecialName);
        }

        /// <summary>
        /// Gets the name of the dynamic assembly.
        /// </summary>
        public string AssemblyName { get; }

        /// <summary>
        /// Gets an instance of the dynamically created type.
        /// </summary>
        public dynamic Instance => Activator.CreateInstance(_typeBuilder.CreateType()!)!;

        /// <summary>
        /// Creates a static method dynamically.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <typeparam name="TReturn">The return type of the method.</typeparam>
        /// <param name="method">The method implementation.</param>
        /// <returns>A compiled delegate representing the static method.</returns>
        public static Func<TParameter, TReturn> CreateStaticMethod<TParameter, TReturn>(
            Func<TParameter, TReturn> method)
        {
            Expression<Func<TParameter, TReturn>> expression = parameter => method(parameter);
            return expression.Compile();
        }

        /// <summary>
        /// Creates a method with a parameter and a return type, including exception handling.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <typeparam name="TReturn">The return type of the method.</typeparam>
        /// <param name="name">The name of the method.</param>
        /// <param name="callback">The method implementation.</param>
        public void CreateMethod<TParameter, TReturn>(string name, Func<TParameter, TReturn> callback)
        {
            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(
                name,
                MethodAttributes.Public,
                typeof(TReturn),
                new[] { typeof(TParameter) });

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            // Local variable to store the result
            ilGenerator.DeclareLocal(typeof(TReturn));

            // Begin exception block
            Label tryBlock = ilGenerator.BeginExceptionBlock();

            // Load arguments and call the method
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Call, callback.Method);
            ilGenerator.Emit(OpCodes.Stloc_0);

            // Leave try block
            ilGenerator.Emit(OpCodes.Leave_S, tryBlock);

            // Begin catch block
            ilGenerator.BeginCatchBlock(typeof(Exception));

            // Handle exception (optional: rethrow or handle accordingly)
            ilGenerator.Emit(OpCodes.Pop); // Pop the exception
            ilGenerator.Emit(OpCodes.Ldnull); // Load default value
            ilGenerator.Emit(OpCodes.Stloc_0); // Store default value

            // End exception block
            ilGenerator.EndExceptionBlock();

            // Return result
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates a method with a return type and no parameters, including exception handling.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the method.</typeparam>
        /// <param name="name">The name of the method.</param>
        /// <param name="callback">The method implementation.</param>
        public void CreateMethod<TReturn>(string name, Func<TReturn> callback)
        {
            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(
                name,
                MethodAttributes.Public,
                typeof(TReturn),
                Type.EmptyTypes);

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            // Local variable to store the result
            ilGenerator.DeclareLocal(typeof(TReturn));

            // Begin exception block
            Label tryBlock = ilGenerator.BeginExceptionBlock();

            // Call the method
            ilGenerator.Emit(OpCodes.Call, callback.Method);
            ilGenerator.Emit(OpCodes.Stloc_0);

            // Leave try block
            ilGenerator.Emit(OpCodes.Leave_S, tryBlock);

            // Begin catch block
            ilGenerator.BeginCatchBlock(typeof(Exception));

            // Handle exception (optional: rethrow or handle accordingly)
            ilGenerator.Emit(OpCodes.Pop); // Pop the exception
            ilGenerator.Emit(OpCodes.Ldnull); // Load default value
            ilGenerator.Emit(OpCodes.Stloc_0); // Store default value

            // End exception block
            ilGenerator.EndExceptionBlock();

            // Return result
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates a method with a parameter and no return type, including exception handling.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="name">The name of the method.</param>
        /// <param name="callback">The method implementation.</param>
        public void CreateMethod<TParameter>(string name, Action<TParameter> callback)
        {
            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(
                name,
                MethodAttributes.Public,
                typeof(void),
                new[] { typeof(TParameter) });

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            // Begin exception block
            ilGenerator.BeginExceptionBlock();

            // Load arguments and call the method
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Call, callback.Method);

            // Leave try block
            ilGenerator.Emit(OpCodes.Leave_S, ilGenerator.DefineLabel());

            // Begin catch block
            ilGenerator.BeginCatchBlock(typeof(Exception));

            // Handle exception (optional: rethrow or handle accordingly)
            ilGenerator.Emit(OpCodes.Pop); // Pop the exception

            // End exception block
            ilGenerator.EndExceptionBlock();

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates an async method with a parameter and a return type.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <typeparam name="TReturn">The return type of the method.</typeparam>
        /// <param name="name">The name of the method.</param>
        /// <param name="callback">The async method implementation.</param>
        public void CreateAsyncMethod<TParameter, TReturn>(string name, Func<TParameter, Task<TReturn>> callback)
        {
            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(
                name,
                MethodAttributes.Public | MethodAttributes.HideBySig,
                typeof(Task<TReturn>),
                new[] { typeof(TParameter) });

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            // Load arguments and call the async method
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Call, callback.Method);
            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates an async method with no parameters and a return type.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the method.</typeparam>
        /// <param name="name">The name of the method.</param>
        /// <param name="callback">The async method implementation.</param>
        public void CreateAsyncMethod<TReturn>(string name, Func<Task<TReturn>> callback)
        {
            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(
                name,
                MethodAttributes.Public | MethodAttributes.HideBySig,
                typeof(Task<TReturn>),
                Type.EmptyTypes);

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            // Call the async method
            ilGenerator.Emit(OpCodes.Call, callback.Method);
            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates an async method with a parameter and no return type.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="name">The name of the method.</param>
        /// <param name="callback">The async method implementation.</param>
        public void CreateAsyncMethod<TParameter>(string name, Func<TParameter, Task> callback)
        {
            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(
                name,
                MethodAttributes.Public | MethodAttributes.HideBySig,
                typeof(Task),
                new[] { typeof(TParameter) });

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            // Load arguments and call the async method
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Call, callback.Method);
            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates an async method with no parameters and no return type.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="callback">The async method implementation.</param>
        public void CreateAsyncMethod(string name, Func<Task> callback)
        {
            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(
                name,
                MethodAttributes.Public | MethodAttributes.HideBySig,
                typeof(Task),
                Type.EmptyTypes);

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            // Call the async method
            ilGenerator.Emit(OpCodes.Call, callback.Method);
            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates a property with the specified name and type.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        public void CreateProperty<T>(string propertyName)
        {
            Type propertyType = typeof(T);
            FieldBuilder fieldBuilder =
                _typeBuilder.DefineField($"_{propertyName}", propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = _typeBuilder.DefineProperty(
                propertyName,
                PropertyAttributes.HasDefault,
                propertyType,
                null);

            // Getter method
            MethodBuilder getMethodBuilder = _typeBuilder.DefineMethod(
                $"get_{propertyName}",
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                propertyType,
                Type.EmptyTypes);

            ILGenerator getIl = getMethodBuilder.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            // Setter method
            MethodBuilder setMethodBuilder = _typeBuilder.DefineMethod(
                $"set_{propertyName}",
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                null,
                new[] { propertyType });

            ILGenerator setIl = setMethodBuilder.GetILGenerator();
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);
            setIl.Emit(OpCodes.Ret);

            // Assign getter and setter
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
        }

        /// <summary>
        /// Creates a field with the specified name and type.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="attributes">The field attributes.</param>
        public void CreateField<T>(string fieldName, FieldAttributes attributes = FieldAttributes.Private)
        {
            _typeBuilder.DefineField(fieldName, typeof(T), attributes);
        }

        /// <summary>
        /// Creates an event with the specified name and delegate type.
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        /// <param name="eventName">The name of the event.</param>
        public void CreateEvent<TDelegate>(string eventName) where TDelegate : Delegate
        {
            FieldBuilder fieldBuilder =
                _typeBuilder.DefineField($"_{eventName}", typeof(TDelegate), FieldAttributes.Private);

            EventBuilder eventBuilder = _typeBuilder.DefineEvent(eventName, EventAttributes.None, typeof(TDelegate));

            // Add method
            MethodBuilder addMethodBuilder = _typeBuilder.DefineMethod(
                $"add_{eventName}",
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig |
                MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final,
                null,
                new[] { typeof(TDelegate) });

            ILGenerator addIl = addMethodBuilder.GetILGenerator();
            addIl.Emit(OpCodes.Ldarg_0);
            addIl.Emit(OpCodes.Ldarg_0);
            addIl.Emit(OpCodes.Ldfld, fieldBuilder);
            addIl.Emit(OpCodes.Ldarg_1);
            addIl.Emit(OpCodes.Call,
                typeof(Delegate).GetMethod("Combine", new[] { typeof(Delegate), typeof(Delegate) })!);
            addIl.Emit(OpCodes.Castclass, typeof(TDelegate));
            addIl.Emit(OpCodes.Stfld, fieldBuilder);
            addIl.Emit(OpCodes.Ret);

            // Remove method
            MethodBuilder removeMethodBuilder = _typeBuilder.DefineMethod(
                $"remove_{eventName}",
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig |
                MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final,
                null,
                new[] { typeof(TDelegate) });

            ILGenerator removeIl = removeMethodBuilder.GetILGenerator();
            removeIl.Emit(OpCodes.Ldarg_0);
            removeIl.Emit(OpCodes.Ldarg_0);
            removeIl.Emit(OpCodes.Ldfld, fieldBuilder);
            removeIl.Emit(OpCodes.Ldarg_1);
            removeIl.Emit(OpCodes.Call,
                typeof(Delegate).GetMethod("Remove", new[] { typeof(Delegate), typeof(Delegate) })!);
            removeIl.Emit(OpCodes.Castclass, typeof(TDelegate));
            removeIl.Emit(OpCodes.Stfld, fieldBuilder);
            removeIl.Emit(OpCodes.Ret);

            // Assign add and remove methods
            eventBuilder.SetAddOnMethod(addMethodBuilder);
            eventBuilder.SetRemoveOnMethod(removeMethodBuilder);
        }

        /// <summary>
        /// Creates a constructor with specified parameter types and implementation.
        /// </summary>
        /// <param name="parameterTypes">The types of the parameters.</param>
        /// <param name="implementation">An action that implements the constructor body using <see cref="ILGenerator"/>.</param>
        public void CreateConstructor(Type[] parameterTypes, Action<ILGenerator> implementation)
        {
            ConstructorBuilder constructorBuilder = _typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                parameterTypes);

            ILGenerator ilGenerator = constructorBuilder.GetILGenerator();
            implementation(ilGenerator);
            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates an operator overload method with the specified name and implementation.
        /// </summary>
        /// <param name="operatorName">The name of the operator method (e.g., "op_Addition").</param>
        /// <param name="parameterTypes">The types of the parameters.</param>
        /// <param name="returnType">The return type of the operator.</param>
        /// <param name="implementation">An action that implements the operator method body using <see cref="ILGenerator"/>.</param>
        public void CreateOperatorOverload(string operatorName, Type[] parameterTypes, Type returnType,
            Action<ILGenerator> implementation)
        {
            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(
                operatorName,
                MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.SpecialName,
                returnType,
                parameterTypes);

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            implementation(ilGenerator);
            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Completes the type creation process and returns the created type.
        /// </summary>
        /// <returns>The dynamically created <see cref="Type"/>.</returns>
        public Type CreateType()
        {
            return _typeBuilder.CreateType()!;
        }
    }
}