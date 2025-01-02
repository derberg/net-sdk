﻿// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Represents the options used to configure <see cref="IAsyncApiDocument"/> generation
/// </summary>
public class AsyncApiDocumentGenerationOptions
{

    /// <summary>
    /// Gets/sets an <see cref="Action{T}"/> used to configure the <see cref="V2AsyncApiDocument"/>s to configure
    /// </summary>
    public Action<IV2AsyncApiDocumentBuilder>? V2BuilderSetup { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="Action{T}"/> used to configure the <see cref="V3AsyncApiDocument"/>s to configure
    /// </summary>
    public Action<IV3AsyncApiDocumentBuilder>? V3BuilderSetup { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the automatically generate examples. Defaults to true.
    /// </summary>
    public bool AutomaticallyGenerateExamples { get; set; } = true;

}