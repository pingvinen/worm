worm
====

ORM for developers that are not afraid of databases.

These tools will generate simple code for your POCOs. The code generation is initiated by the developer and the generated code can easily be overruled/modified by the developer.

It is designed for use with Dependency Injection and Mocking, and to be used in software that connects to multiple databases (possibly with a variety of databases).

It does not handle actual relations at this point. If you really need that and caching etc. you should look into something like NHibernate instead.

You will end up writing queries to select stuff from the database, but populating instances and saving is handled by Worm.



Platform support
----------

I hope this will work equally well on both Microsoft .NET and Mono, but I only really test it on Linux/Mono.


Versioning
----------

Releases will be numbered with the follow format:

`<major>.<minor>.<patch>`

And constructed with the following guidelines:

* Addition of new namespaces bumps the major
* Additions in existing namespaces bumps the minor
* Bug fixes and misc that does not change the public API bump the patch

I strive to never break backwards-compatibility. Once the API is public it must not change.

Some APIs might need to be replaced over time. This will be done by adding new APIs with different names
and marking the old APIs as "deprecated" and later as "obsolete", and only removing them from the code if
we learn that no one is using them.

**The APIs should be considered unstable until version 1.0.0**

Until version 1.0.0 namespace additions will bump the minor.
