// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Stact.FileSystem.Internal
{
	using Events;

	public class FileRenamedImpl :
		FileRenamed
	{
		public FileRenamedImpl(string name, string path, string oldName, string oldPath)
		{
			Name = name;
			Path = path;
			OldName = oldName;
			OldPath = oldPath;

			EventType = FileSystemEventType.Renamed;
		}

		public FileSystemEventType EventType { get; private set; }

		public string Path { get; set; }
		public string Name { get; set; }
		public string OldPath { get; set; }
		public string OldName { get; set; }
	}
}