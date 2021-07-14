﻿/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using FluentValidation;
using Neuroglia.AsyncApi.Models;
using System;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IVariableBuilder"/> interface
    /// </summary>
    public class VariableBuilder
        : IVariableBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="VariableBuilder"/>
        /// </summary>
        /// <param name="validators">The services used to validate <see cref="Models.Variable"/>s</param>
        public VariableBuilder(IEnumerable<IValidator<Variable>> validators)
        {
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the services used to validate <see cref="Models.Variable"/>s
        /// </summary>
        protected virtual IEnumerable<IValidator<Variable>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="Models.Variable"/> to build
        /// </summary>
        protected Variable Variable { get; } = new();

        /// <inheritdoc/>
        public virtual IVariableBuilder WithEnumValues(params string[] values)
        {
            this.Variable.Enum = values;
            return this;
        }

        /// <inheritdoc/>
        public virtual IVariableBuilder WithDefaultValue(string value)
        {
            this.Variable.Default = value;
            return this;
        }

        /// <inheritdoc/>
        public virtual IVariableBuilder WithDescription(string description)
        {
            this.Variable.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual IVariableBuilder AddExample(string example)
        {
            if (string.IsNullOrWhiteSpace(example))
                throw new ArgumentNullException(nameof(example));
            if (this.Variable.Examples == null)
                this.Variable.Examples = new();
            this.Variable.Examples.Add(example);
            return this;
        }

        /// <inheritdoc/>
        public virtual Variable Build()
        {
            return this.Variable;
        }
    }

}
