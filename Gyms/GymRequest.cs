using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymAndYou___MinimalAPI___Project.Gyms
{
    public static class GymRequest
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapGet("/api/gym",GymRequest.GetAll)
                .WithDescription("Return every gyms from resources")
                .WithTags("Gyms")
                .Produces<List<Gym>>();
            app.MapGet("/api/gym/{id}",GymRequest.GetById)
                .WithTags("Gyms")
                .WithSummary("Return particular gym from resources")
                .Produces<Gym>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);
            app.MapPost("/api/gym",GymRequest.Create)
                .WithTags("Gyms")
                .WithSummary("Add gym to resources")
                .Accepts<Gym>("application/json")
                .Produces<Guid>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);
            app.MapPut("/api/gym/{id}",GymRequest.Update)
                .WithTags("Gyms")
                .Accepts<Gym>("application/json")
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status200OK);
            app.MapDelete("/api/gym/{id}",GymRequest.Delete)
                .WithTags("Gyms")
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status200OK);
            return app;
        }

        private static IResult GetAll(GymService service)
        {
            var gyms = service.GetGyms();
            return Results.Ok(gyms);
        }
        private static IResult GetById(GymService service, Guid id)
        {
            var gym = service.Get(id);

            if(gym is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(gym);
        }
        private static IResult Create(GymService service,Gym gym, IValidator<Gym> validator)
        {
            var validationResoult = validator.Validate(gym);

            if(!validationResoult.IsValid)
            {
                return Results.BadRequest(validationResoult.Errors);
            }
            service.Create(gym);
            return Results.Ok(gym.Id);
        }
        private static IResult Update(GymService service,Gym gym,Guid id, IValidator<Gym> validator)
        {
            var validationResoult = validator.Validate(gym);

            if(!validationResoult.IsValid)
            {
                return Results.BadRequest(validationResoult.Errors);
            }

            var existingGym = service.Get(id);
            if(existingGym is null)
            {
                return Results.NotFound();
            }

            service.Update(id,gym);
            return Results.Ok();
        }
        private static IResult Delete(GymService service, Guid id)
        {
            var existingGym = service.Get(id);

            if(existingGym is null)
            {
                return Results.NotFound();
            }

            service.Delete(id);
            return Results.Ok();
        }
    }
}
