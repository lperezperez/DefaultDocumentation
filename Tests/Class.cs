#nullable enable
namespace DotNetToGitHubWiki
{
    using System;
    using System.Threading.Tasks;
    /// <summary>
    ///     A Dummy <see langword="class"/> whose purpose is to implement all the <see href="https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments">recommended tags for documentation comments</see>. <list type="table">
    ///         <listheader>
    ///             <term>term</term> <description>description</description>
    ///         </listheader> <item>
    ///             <term>term1</term> <description>description1</description>
    ///         </item> <item>
    ///             <term>term2</term> <description>description2</description>
    ///         </item> <item>
    ///             <term>term3</term> <description>description3</description>
    ///         </item>
    ///     </list>
    /// </summary>
    public sealed class Class
    {
        #region Constants
        /// <summary>
        ///     Represents a <see href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/constants">constant</see>.
        /// </summary>
        public const string DummyConst = "Dummy";
        #endregion
        #region Fields
        /// <summary>
        ///     Represents a <see href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/fields">field</see>.
        /// </summary>
        public int DummyField;
        #endregion
        #region Properties
        /// <summary>
        ///     Represents a <see href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/properties">property</see>.
        /// </summary>
        public TaskContinuationOptions DummyOption { get; }
        /// <summary>
        ///     Represents a <see href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/properties">property</see>.
        /// </summary>
        public int DummyProperty { get; }
        #endregion
        #region Indexers
        /// <summary>Represents an <see href="https://docs.microsoft.com/dotnet/csharp/programming-guide/indexers">indexed</see>.</summary>
        /// <param name="index">The index of the <see langword="dynamic"/> instance to retrieve.</param>
        /// <value>The <see langword="dynamic"/> instance at the specified <paramref name="index"/>.</value>
        public dynamic this[int index] => index;
        #endregion
        #region Methods
        /// <summary>Computes the sum of <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Class operator +(Class left, int right) => left;
        /// <summary>Computes the logical AND of <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns>The logical AND of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static bool operator &(Class left, Class right) => true;
        /// <summary>
        ///     Evaluates both <paramref name="left"/> and <paramref name="right"/> even if <paramref name="left"/> evaluates to <c>true</c>, so that the operation result is <c>true</c> regardless of the value of <paramref name="right"/>.
        /// </summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is evaluated as <c>true</c> , otherwise, <c>false</c>.</returns>
        public static bool operator |(Class left, Class right) => true;
        /// <summary>Decrements <paramref name="class"/> by 1.</summary>
        /// <param name="class">A <see cref="Class"/> instance.</param>
        /// <returns>
        ///     If prefixed, the value of <paramref name="class"/> after the operation, otherwise the value of <paramref name="class"/> before the operation.
        /// </returns>
        public static Class operator --(Class @class) => @class;
        /// <summary>Divides <paramref name="left"/> by <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns>The result of <paramref name="left"/> divided by <paramref name="right"/>.</returns>
        public static Class operator /(Class left, Class right) => left;
        /// <summary>Compares <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal, otherwise, <c>false</c>.</returns>
        public static bool operator ==(Class left, Class right) => true;
        /// <summary>
        ///     Computes the logical exclusive OR, also known as the logical XOR, of <paramref name="left"/> and <paramref name="right"/>.
        /// </summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="left"/> evaluates to <c>true</c> and <paramref name="right"/> evaluates to <c>false</c>, or <paramref name="left"/> evaluatess to <c>false</c> and <paramref name="right"/> evaluates to <c>true</c>. Otherwise, the result is <c>false</c>.
        /// </returns>
        public static bool operator ^(Class left, Class right) => true;
        /// <summary>Converts the <paramref name="class"/> to a <see cref="double"/> instance.</summary>
        /// <param name="class">A <see cref="Class"/> instance.</param>
        /// <returns>A <see cref="double"/> instance.</returns>
        public static explicit operator double(Class @class) => 0;
        /// <summary>Converts the <paramref name="integer"/> to a <see cref="Class"/> instance.</summary>
        /// <param name="integer">An <see cref="int"/> instance.</param>
        /// <returns>A <see cref="Class"/> instance.</returns>
        public static explicit operator Class(int integer) => new Class();
        /// <summary>Compares <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>, otherwise, <c>false</c>.</returns>
        public static bool operator >(Class left, Class right) => false;
        /// <summary>Compares <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>, otherwise, <c>false</c>.</returns>
        public static bool operator >=(Class left, Class right) => false;
        /// <summary>Increments <paramref name="class"/> by 1.</summary>
        /// <param name="class">A <see cref="Class"/> instance.</param>
        /// <returns>
        ///     If prefixed, the value of <paramref name="class"/> after the operation, otherwise the value of <paramref name="class"/> before the operation.
        /// </returns>
        public static Class operator ++(Class @class) => @class;
        /// <summary>Compares <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns><c>false</c> if <paramref name="left"/> and <paramref name="right"/> are equal, otherwise, <c>true</c>.</returns>
        public static bool operator !=(Class left, Class right) => false;
        /// <summary>Shifts <paramref name="class"/> left by the number of bits defined by <paramref name="integer"/>.</summary>
        /// <param name="class">A <see cref="Class"/> instance.</param>
        /// <param name="integer">An <see cref="int"/> instance.</param>
        /// <returns>An <see cref="int"/> instance.</returns>
        public static Class operator <<(Class @class, int integer) => @class;
        /// <summary>Compares <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>, otherwise, <c>false</c>.</returns>
        public static bool operator <(Class left, Class right) => false;
        /// <summary>Compares <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>, otherwise, <c>false</c>.</returns>
        public static bool operator <=(Class left, Class right) => false;
        /// <summary>Computes the remainder after dividing <paramref name="left"/> by <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns>The remainder after dividing <paramref name="left"/> by <paramref name="right"/>.</returns>
        public static Class operator %(Class left, Class right) => left;
        /// <summary>Computes the product of <paramref name="left"/> and <paramref name="right"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Class operator *(Class left, Class right) => left;
        /// <summary>Produces a bitwise complement of <paramref name="class"/> by reversing each bit.</summary>
        /// <param name="class">A <see cref="Class"/> instance.</param>
        /// <returns>A bitwise complement of <paramref name="class"/>.</returns>
        public static Class operator ~(Class @class) => @class;
        /// <summary>Shifts <paramref name="class"/> right by the number of bits defined by <paramref name="integer"/>.</summary>
        /// <param name="class">A <see cref="Class"/> instance.</param>
        /// <param name="integer">An <see cref="int"/> instance.</param>
        /// <returns>An <see cref="int"/> instance.</returns>
        public static Class operator >>(Class @class, int integer) => @class;
        /// <summary>Subtracts <paramref name="right"/> from <paramref name="left"/>.</summary>
        /// <param name="left">Left operator parameter.</param>
        /// <param name="right">Right operator parameter.</param>
        /// <returns>The result of <paramref name="left"/> minus <paramref name="right"/>.</returns>
        public static Class operator -(Class left, Class right) => left;
        /// <summary>Computes the numeric negation of <paramref name="class"/>.</summary>
        /// <param name="class">A <see cref="Class"/> instance.</param>
        /// <returns>The <paramref name="class"/> negated.</returns>
        public static Class operator -(Class @class) => @class;
        /// <summary>Returns the value of <paramref name="class"/>.</summary>
        /// <param name="class">A <see cref="Class"/> instance.</param>
        /// <returns>Yhe value of <paramref name="class"/>.</returns>
        public static Class operator +(Class @class) => @class;
        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="object">The object to compare with the current object.</param>
        /// <returns>
        ///     <see langword="true"/> if the specified <paramref name="object"/> is equal to the current <see cref="Class"/> instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object? @object) => base.Equals(@object);
        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
        /// <summary>dummy</summary>
        /// <typeparam name="T">dummy</typeparam>
        /// <param name="value">dummy</param>
        /// <returns>dummy</returns>
        public async Task<dynamic> DummyAsync<T>(T value)
        {
            await Task.Delay(0);
            return value ?? default(T);
        }
        /// <summary>dummy</summary>
        /// <typeparam name="T2">dummy</typeparam>
        /// <param name="pouet">dummy</param>
        /// <returns>dummy</returns>
        public ValueTuple<int, Class> DummyExplicitTuple<T2>(T2 pouet) => (42, this);
        /// <summary>dummy</summary>
        /// <param name="pouet">kikoo</param>
        /// <typeparam name="T2">lol</typeparam>
        public void DummyMethod<T2>(T2 pouet)
        {
            var t = this;
            t += 0;
        }
        /// <summary>dummy</summary>
        /// <typeparam name="T2">dummy</typeparam>
        /// <param name="pouet">dummy</param>
        /// <returns>dummy</returns>
        public (int, Class) DummyTuple<T2>(T2 pouet) => (42, this);
        /// <summary>dummy</summary>
        /// <param name="p">dummy</param>
        /// <returns>dummy</returns>
        public unsafe int** Unsafe(void* p) => (int**)&p;
        #endregion
        #region Classes
        /// <summary>
        ///     dummy <c>c tag</c> line break
        ///     <para>para tag.</para>
        ///     <see cref="Class.DummyOption"/> <code>
        /// yep
        /// </code>
        /// </summary>
        /// <typeparam name="T">The <see langword="object"/> type.</typeparam>
        /// <remarks>pouet</remarks>
        /// <example>var dn = new DummyNested&lt;object&gt;();</example>
        public class DummyNested<T>
        {
            #region Events
            /// <summary>dummy</summary>
            public event Action<T> Action;
            #endregion
        }
        #endregion
    }
}