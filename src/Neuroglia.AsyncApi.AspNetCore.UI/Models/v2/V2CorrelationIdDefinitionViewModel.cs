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

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models.v2;

/// <summary>
/// Holds the data used to render an <see cref="V2CorrelationIdDefinition"/> view
/// </summary>
public record V2CorrelationIdDefinitionViewModel
    : V2AsyncApiDocumentViewModel
{

    /// <inheritdoc/>
    public V2CorrelationIdDefinitionViewModel(V2AsyncApiDocument document, V2CorrelationIdDefinition correlationId) : base(document) { CorrelationId = correlationId; }

    /// <summary>
    /// Gets the <see cref="V2CorrelationIdDefinition"/> to render
    /// </summary>
    public V2CorrelationIdDefinition CorrelationId { get; }


}
