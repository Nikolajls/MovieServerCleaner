using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieServerCleaner
{
    public class FileSystemEnumerable : IEnumerable<FileSystemInfo>
    {
        private readonly int _maxrecursive;
        private readonly SearchOption _option;
        private readonly IList<string> _patterns;

        private readonly DirectoryInfo _root;
        private readonly int _current;

        public FileSystemEnumerable(DirectoryInfo root, string pattern, SearchOption option, int maxrecursive, int current = 0)
        {
            _root = root;
            _patterns = new List<string> {pattern};
            _option = option;
            _maxrecursive = maxrecursive;
            _current = current;
        }

        public FileSystemEnumerable(DirectoryInfo root, IList<string> patterns, SearchOption option, int maxrecursive, int current = 0)
        {
            _root = root;
            _patterns = patterns;
            _option = option;
            _maxrecursive = maxrecursive;
            _current = current;
        }

        public IEnumerator<FileSystemInfo> GetEnumerator()
        {
            if ((_root == null) || !_root.Exists) yield break;

            IEnumerable<FileSystemInfo> matches = new List<FileSystemInfo>();
            try
            {
                foreach (var pattern in _patterns)
                    matches = matches.Concat(_root.EnumerateDirectories(pattern, SearchOption.TopDirectoryOnly)).Concat(_root.EnumerateFiles(pattern, SearchOption.TopDirectoryOnly));
            }
            catch (UnauthorizedAccessException)
            {
                yield break;
            }
            catch (PathTooLongException)
            {
                yield break;
            }
            catch (IOException)
            {
                yield break;
            }

            foreach (var file in matches)
                yield return file;

            if (_option == SearchOption.AllDirectories)
                foreach (var dir in _root.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
                    if (_current < _maxrecursive)
                    {
                        var fileSystemInfos = new FileSystemEnumerable(dir, _patterns, _option, _maxrecursive, _current + 1);
                        foreach (var match in fileSystemInfos)
                            yield return match;
                    }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}