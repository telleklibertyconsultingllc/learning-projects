# Best Practices For Microservices

- Organizations that have successfully laid a foundation for continuous innovation and agility have adopted microservice architectures to respond rapidly to the never-ending demands of the business.

## What are microservices?

- Enterprise Integration was a major force for the lack of alignment between customer-facing business process led to duplicate efforts and missing or inaccurate information in each solution.
- Microservices are most often directly invoked by HTTP REST API calls.
- RAML (Restful API Modeling Language) allow for the formal definition of REST APIs. RAML can define every resource and operation exposed by the microservice.
- Definitions
-- Asset = is primarily an encapsulation of a business entity capability, like customer, order or invoice
-- System API = system-level microservices are in line with the concept of an autonomous service which has been designed with enough abstraction to hide the underlying systems of record. The responsibility of the API is discrete and agnostic to any particular business process.
-- System APIs are composable with other APIs to form aggregate Process APIs. The composition of System APIs can take the form of explicit API orchestration (direct calls) or through the more reliable API choreography by which they are driven by business events relevant to the context of the composition.
-- Experience API = directly interfaces with the channel of the experience and is in many cases externally facing to consumers outside of the organization.

APP DEV     => Experience APIs (Purpose-Built for Apps)
LOB/DEV IT  => PROCESS APIs (Orchestration, Composable APIs, Microservices)
CENTRAL IT  => SYSTEM APIs (Legacy Modernization, Connectivity to SaaS APPs, Web Services and RESTFul APIs)

- Microservices are the evolution of best-practice architectural principles that shape the delivery of solutions to the business in the form of services.

## Best Practice 1

- Apply an API gateway in front of services with external consumers.

## Best Practice 2

- A microservice should not be reduced to a simple CRUD service. Each entity encapsulates within itself all responsibilities relevant to the business domain for which it was designed.

## Business Domain Orientation of MicroService Architecture

- It is important to approach service design for a particular domain and NOT insist on doing so for every aspect of the business.
- Reuse
- A microservice architecture, in concert with cloud deployment technologies, API management, and integration technologies, provides an alternate approach to software development which avoids the delivery of monolithic applications. The monolith is instead broken up into a set of independent services that are developed, deployed and maintained separately. Advantages:
-- Services are encouraged to be small, ideally built by a handful of developers that can be fed by 2 pizza boxes. A small group or single developer can understand the whole service.
-- If microservices expose their interfaces with a standard protocol, such as REST API or an AMQP exchange, they can be consumed and resuded by other services and applications without direct coupling through language bindings or shared libraries.
-- Services exist as independent deployment artifacts and can be scaled independently of other services.
-- Developing services discretely allows developers to use the appropriate development framework for the task at hand. Services that compose other services into a composite API might be quicker to implement with MuleSoft than .NET or JEE
-- The tradeoff of this flexibility is complexity. Managing a multitude of distributed services at scale is difficult.
-- Project teams need to easily discover services as potential reuse candidates. These services should provide documentation, test consoles, etc so reusing is significantly easier than building from scratch.
-- Interdependencies between services need to be closely monitored. Downtime of services, service outage, service upgrades, etc can all have cascading downstream effects and such impact should be proactively analyzed.

## Message Broker
