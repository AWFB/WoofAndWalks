﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoofsAndWalksAPI.Data;
using WoofsAndWalksAPI.DTOs;
using WoofsAndWalksAPI.Extensions;
using WoofsAndWalksAPI.Helpers;
using WoofsAndWalksAPI.Interfaces;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Controllers;

[Authorize]
public class MessagesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MessagesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // [HttpPost]
    // public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    // {
    //     var username = User.GetUserName();
    //     if (username == createMessageDto.RecipientUsername.ToLower())
    //         return BadRequest("You can not message yourself");
    //
    //     var sender = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
    //     var recipient = await _unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);
    //
    //     if (recipient == null) return NotFound();
    //
    //     var message = new Message
    //     {
    //         Sender = sender,
    //         Recipient = recipient,
    //         SenderUsername = sender.UserName,
    //         RecipientUsername = recipient.UserName,
    //         Content = createMessageDto.Content
    //     };
    //     
    //     _unitOfWork.MessageRepository.AddMessage(message);
    //
    //     if (await _unitOfWork.MessageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));
    //
    //     return BadRequest("Failed to send message");
    // }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]MessageParams messageParams)
    {
        messageParams.Username = User.GetUserName();

        var messages = await _unitOfWork.MessageRepository.GetMessagesForUser(messageParams);
        
        Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

        return messages;

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id)
    {
        var username = User.GetUserName();
        var message = await _unitOfWork.MessageRepository.GetMessage(id);

        if (message.Sender.UserName != username && message.Recipient.UserName != username) return Unauthorized();

        if (message.Sender.UserName == username) message.SenderDeleted = true;

        if (message.Recipient.UserName == username) message.RecipientDeleted = true;
        
        if (message.SenderDeleted && message.RecipientDeleted) _unitOfWork.MessageRepository.DeleteMessage(message);

        if (await _unitOfWork.Complete()) return Ok();

        return BadRequest("Problem deleting message");
    }
}