// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Uol.PagSeguro.Test
{
    internal static class TestHelper
    {
        internal static void ValidateErrors(IEnumerable<PagSeguroServiceError> errors, string[] expected)
        {
            if (errors == null)
                throw new ArgumentNullException("errors");
            if (expected == null)
                throw new ArgumentNullException("expected");

            foreach (PagSeguroServiceError error in errors)
            {
                bool found = false;
                foreach (string code in expected)
                {
                    if (code == error.Code)
                    {
                        found = true;
                        break;
                    }
                }
                Assert.True(found, String.Format("Unexpected PagSeguroServiceError found '{0}'", error.Code));
            }
        }
    }
}
