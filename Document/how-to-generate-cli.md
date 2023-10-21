# How-To

This document describes how to develop for `BSN.IpTables.Cli`

## Some considration

* warning | PreCheck/AllOfWhenYouMeantRef _is using an 'allOf' instead of a $ref. This creates a wasteful anonymous type when generating code._
  * Resolve warning: as you can see in <https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1488> and <https://github.com/unchase/Unchase.Swashbuckle.AspNetCore.Extensions/issues/13> we must to do not use `options.UseAllOfToExtendReferenceSchemas();` in my code.
