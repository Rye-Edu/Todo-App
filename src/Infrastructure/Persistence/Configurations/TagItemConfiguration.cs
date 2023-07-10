using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo_App.Domain.Entities;

namespace Todo_App.Infrastructure.Persistence.Configurations;
public class TagItemConfiguration : IEntityTypeConfiguration<TagItem>
{
    public void Configure(EntityTypeBuilder<TagItem> builder)
    {
        builder.HasMany(todoItem => todoItem.TodoItems).WithMany(tagItem => tagItem.TagItems);
    }
}
