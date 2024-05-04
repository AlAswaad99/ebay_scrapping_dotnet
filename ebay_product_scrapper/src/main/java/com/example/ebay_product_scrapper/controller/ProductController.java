package com.example.ebay_product_scrapper.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.example.ebay_product_scrapper.dto.RequestDTO;
import com.example.ebay_product_scrapper.dto.ResponseDTO;
import com.example.ebay_product_scrapper.service.ProductScrapperService;

@RestController
public class ProductController {

    @Autowired
    private ProductScrapperService productScrapperService;

    @PostMapping("/scrape-products-list")
    public ResponseEntity<ResponseDTO> scrapeProductsList(@RequestBody RequestDTO requestDTO) {
        ResponseDTO res = productScrapperService.scrapeProductsList(requestDTO);
        if (res.getResponseCode() == 201)
            return ResponseEntity.ok(res);

        return ResponseEntity.status(res.getResponseCode()).body(res);
    }

    @PostMapping("/scrape-all-products-detail")
    public ResponseEntity<ResponseDTO> scrapeAllProductsDetail() {
        ResponseDTO res = productScrapperService.scrapreAllProductsDetails();
        if (res.getResponseCode() == 201)
            return ResponseEntity.ok(res);

        return ResponseEntity.status(res.getResponseCode()).body(res);
    }

    @PostMapping("/scrape-product-detail")
    public ResponseEntity<ResponseDTO> scrapeProductsDetail(@RequestBody RequestDTO requestDTO) {
        ResponseDTO res = productScrapperService.scrapreProductDetails(requestDTO.getItemNumber());
        if (res.getResponseCode() == 201)
            return ResponseEntity.ok(res);

        return ResponseEntity.status(res.getResponseCode()).body(res);
    }

}
