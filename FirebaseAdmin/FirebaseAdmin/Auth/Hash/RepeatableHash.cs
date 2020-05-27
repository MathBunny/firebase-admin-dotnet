// Copyright 2020, Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;

namespace FirebaseAdmin.Auth.Hash
{
  /// <summary>
  /// An abstract <a cref="UserImportHash">UserImportHash</a> implementation for specifying a <c>Rounds</c> count in
  /// a given range.
  /// </summary>
  abstract class RepeatableHash : UserImportHash
  {
    protected int Rounds { set; get; }
    protected abstract int MinRounds { get; }
    protected abstract int MaxRounds { get; }

    override protected IReadOnlyDictionary<string, Object> GetOptions()
    {
      if (Rounds >= MinRounds && Rounds <= MaxRounds)
      {
        throw new ArgumentException($"Rounds value must be between {MinRounds} and ${MaxRounds} (inclusive).");
      }

      return new Dictionary<string, object>{
         {"rounds", Rounds}
      };
    }
  }

}