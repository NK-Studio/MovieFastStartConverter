using System.Linq;

namespace NKStudio
{
    public class ExtensionFilter
    {
        private string Description { get; }
        private string[] Extensions { get; }
        
        /// <summary>
        /// ExtensionFilter 클래스의 생성자입니다.
        /// </summary>
        /// <param name="description">파일 형식에 대한 설명입니다. (MP4 Files, Image Files)</param>
        /// <param name="extensions">파일 확장자의 가변 인자 목록입니다. (mp4, avi, png, jpg)</param>
        public ExtensionFilter(string description, params string[] extensions)
        {
            Description = description;
            Extensions = extensions;
        }
        
        public override string ToString()
        {
            return $"{Description}\0*." + string.Join(";*.", Extensions) + "\0";
        }
        
        internal static string ToString(ExtensionFilter[] filters)
        {
            return string.Join("", filters.Select(f => f.ToString()));
        }
    }
}