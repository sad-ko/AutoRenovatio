namespace AutoRenovatioNS.Interfaces;

using Parsers;

/// <summary>
/// Interface that defines the necessary methods to generate objects that use the <see cref="IUpdateInfo"/> interface.
/// </summary>
/// <summary xml:lang="es">
/// Interfaz que define los metodos necesarios para poder generar objectos que utilicen la interfaz <see cref="IUpdateInfo"/>.
/// </summary>
public interface IParser<out T> where T : IUpdateInfo, new()
{
    /// <summary>
    /// Converts <paramref name="content"/> to an object of type <typeparamref name="T"/> that implements the interface <see cref="IUpdateInfo"/>.
    /// <br/><br/>
    /// See <see cref="XmlParser{T}"/>, <see cref="JsonParser{T}"/> or <see cref="YamlParser{T}"/>
    /// for some examples on how to implement it.
    /// </summary>
    /// <summary xml:lang="es">
    /// Convierte <paramref name="content"/> a un objeto de tipo <typeparamref name="T"/> que implementa la interfaz <see cref="IUpdateInfo"/>.
    /// <br/><br/>
    /// Ejemplos de como implementarlo:
    /// <see cref="XmlParser{T}"/>, <see cref="JsonParser{T}"/> o <see cref="YamlParser{T}"/>
    /// </summary>
    /// <returns>Object that implements <see cref="IUpdateInfo"/></returns>
    /// <returns xml:lang="es">Objeto que implementa la interfaz <see cref="IUpdateInfo"/></returns>
    public T? Parse(string content);
}